using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace ElderlyCare.Workers.RobotHandler
{
    public class OpcApplication
    {
        public ApplicationInstance Application { get; private set; }
        public ApplicationConfiguration Configuration { get; private set; }
        public int Timeout { get; set; } = 10000;

        public OpcApplication(string applicationName)
        {
            var application = new ApplicationInstance
            {
                ApplicationName = applicationName,
                ApplicationType = ApplicationType.Client,
            };

            var configuration = new ApplicationConfiguration
            {
                ClientConfiguration = new ClientConfiguration
                {
                    MinSubscriptionLifetime = 10000,
                    DefaultSessionTimeout = Timeout,
                },
                SecurityConfiguration = new SecurityConfiguration(),
                CertificateValidator = new CertificateValidator(),
                TransportQuotas = new TransportQuotas
                {
                    OperationTimeout = Timeout,
                    MaxStringLength = 1048576,
                    MaxByteStringLength = 4194304,
                    MaxArrayLength = 65535,
                    MaxMessageSize = 4194304,
                    MaxBufferSize = 65535,
                    ChannelLifetime = 300000,
                    SecurityTokenLifetime = 3600000,
                }
            };

            configuration.SecurityConfiguration.AutoAcceptUntrustedCertificates = true;
            configuration.CertificateValidator.CertificateValidation += OnCertificateValidation;

            application.ApplicationConfiguration = Configuration = configuration;
            Application = application;
        }

        private void OnCertificateValidation(CertificateValidator sender, CertificateValidationEventArgs e)
        {
            e.Accept = true;
        }

        public Session CreateSession(string endpoint)
        {
            var endpointConfiguration = EndpointConfiguration.Create(Configuration);
            var endpointSelection = CoreClientUtils.SelectEndpoint(endpoint, false, Timeout);
            var configuredEndpoint = new ConfiguredEndpoint(null, endpointSelection, endpointConfiguration);

            X509Certificate2 clientCertificate = null;
            X509Certificate2Collection clientCertificateChain = null;

            if (configuredEndpoint.Description.SecurityPolicyUri != SecurityPolicies.None)
            {
                if (Configuration.SecurityConfiguration.ApplicationCertificate == null)
                {
                    throw ServiceResultException.Create(Opc.Ua.StatusCodes.BadConfigurationError, "ApplicationCertificate must be specified.");
                }

                clientCertificate = Configuration.SecurityConfiguration.ApplicationCertificate.Find(true).Result;

                if (clientCertificate == null)
                {
                    throw ServiceResultException.Create(Opc.Ua.StatusCodes.BadConfigurationError, "ApplicationCertificate cannot be found.");
                }
            }

            var messageContext = Configuration.CreateMessageContext(true);

            ITransportChannel channel = SessionChannel.Create(
                Configuration,
                configuredEndpoint.Description,
                configuredEndpoint.Configuration,
                clientCertificate,
                Configuration.SecurityConfiguration.SendCertificateChain ? clientCertificateChain : null,
                messageContext);

            return new Session(channel, Configuration, configuredEndpoint, null);
        }
    }
}
