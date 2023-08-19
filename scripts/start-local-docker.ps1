cd ..\docker\develop
docker compose -p elderly_care -f db.yml -f api.yml -f workers.yml -f opcua-server.yml up -d
cd ..\..\scripts