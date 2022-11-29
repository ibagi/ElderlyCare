import { Circle, Layer, Text, Group } from "react-konva"
import { Robot } from "../../../api/generated"
import { useFloorMapState } from "../FloorMapState"

export default function RobotsLayer() {
    const robots = useFloorMapState(state => state.robots)
    const robotStates = useFloorMapState(state => state.robotStates)

    if (!robots) {
        return null
    }

    function getRobotState(robot: Robot) {
        if (!robotStates) {
            return robot.state
        }

        const liveState = robotStates[robot.robotId ?? '']
        if (liveState) {
            return liveState
        }

        return robot.state
    }

    const robotIndicators = robots.map(r => (
        <Group key={r.robotId} x={getRobotState(r)?.PosX ?? 0} y={getRobotState(r)?.PosY ?? 0} >
            <Circle radius={7} fill="#00b572" />
            <Text y={10} text={r.robotName ?? ''} />
        </Group>
    ))

    return (
        <Layer>
            {robotIndicators}
        </Layer>
    )
}