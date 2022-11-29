import { Drawer, } from "@mui/material"
import { useFloorMapState } from "./FloorMapState"

export default function FloorMapDetails() {
    const selectedRobot = useFloorMapState(state => state.selectedRobot)
    const robotStates = useFloorMapState(state => state.robotStates)
    const selectRobot = useFloorMapState(state => state.selectRobot)

    if (!selectedRobot) {
        return null
    }

    const currentState = robotStates[selectedRobot.robotId ?? '']
    const state = !currentState ? null : (
        <pre>
            {JSON.stringify(currentState, null, 2)}
        </pre>
    )

    return (
        <Drawer open={true} anchor="right" onClose={() => selectRobot(null)} >
            <div className="w-64 flex flex-col items-center">
                <div className="py-1 px-2 mt-5 w-28 font-bold">
                    {selectedRobot.robotName}
                </div>
                <div className="bg-teal-500 text-center text-white py-1 px-2 w-28">
                    {currentState.Status}
                </div>
                {state}
            </div>

        </Drawer>
    )
}