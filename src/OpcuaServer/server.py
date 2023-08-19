# This example is made by using these open source libs:
# https://github.com/OPCFoundation/UA-Nodeset/
# https://github.com/FreeOpcUa/opcua-asyncio/
import asyncio
import random
import logging
from asyncua import ua, Server
from asyncua.common.instantiate_util import instantiate

_logger = logging.getLogger(__name__)

ROBOT_STATES = ['Idle', 'Moving']
NUMBER_OF_ROBOTS = 5

async def update_variable(idx, robot, key, value):
    ref = await robot.get_child(key)
    await ref.write_value(value)

async def update_robot_state(idx, robot):
    await update_variable(idx, robot, 'pos_x', float(random.randint(0, 1000)))
    await update_variable(idx, robot, 'pos_y', float(random.randint(0, 1000)))
    await update_variable(idx, robot, 'status', random.choice(ROBOT_STATES))

async def main():
    # setup our server
    server = Server()
    await server.init()

    server.set_endpoint("opc.tcp://0.0.0.0:4840/")
    server.set_server_name("Mock Opcua Server")
    server.set_security_policy(
        [
            ua.SecurityPolicyType.NoSecurity
        ]
    )

    # setup our own namespace
    uri = "http://mock.opcua.server"
    idx = await server.register_namespace(uri)
    print(type(idx))

    # First a folder to organise our nodes
    folder = await server.nodes.objects.add_folder("ns=2;s=/Robots", "Robots")
    robots = []

    for i in range(NUMBER_OF_ROBOTS):
        # create a new node type we can instantiate in our address space
        robot = await server.nodes.base_object_type.add_object_type(idx, "Robot")
        robot_name = f"Robot{str((i + 1)).zfill(5)}"
        await (await robot.add_property(f'ns=2;s=/Robots/{robot_name}/robot_id;', "robot_id", f"{i + 1}")).set_modelling_rule(True)
        await (await robot.add_property(f'ns=2;s=/Robots/{robot_name}/name;', "name", robot_name)).set_modelling_rule(True)
        await (await robot.add_property(f'ns=2;s=/Robots/{robot_name}/status;', "status", "Idle")).set_modelling_rule(True)
        await (await robot.add_variable(f'ns=2;s=/Robots/{robot_name}/pos_x;', "pos_x", 1.0)).set_modelling_rule(True)
        await (await robot.add_variable(f'ns=2;s=/Robots/{robot_name}/pos_y;', "pos_y", 1.0)).set_modelling_rule(True)
        robot_instance = await folder.add_object(f'ns=2;s=/Robots/{robot_name};', robot_name, robot)
        robots.append(robot_instance)

    # starting!
    async with server:
        while True:
            await asyncio.sleep(1)
            for robot in robots:
                await update_robot_state(idx, robot)


if __name__ == "__main__":
    logging.basicConfig(level=logging.INFO)
    asyncio.run(main())