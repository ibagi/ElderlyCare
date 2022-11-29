import create from "zustand";

import { FloorPlan, Robot } from "../../api/generated";
import ElderlyCareApi from "../../api";

export type LiveRobotState = {
    RobotId: string;
    State: any;
}

export interface FloorMapState {
    robots: Robot[];
    robotStates: Record<string, any>;
    floorPlans: FloorPlan[];
    selectedFloorPlan: FloorPlan | null;
    selectedRobot: Robot | null;
    
    loadFloorPlan(): Promise<void>;
    startLiveUpdate(): void;
    stopLiveUpdate(): void;
    selectRobot: (robot: Robot | null) => void;
}

let eventSource: EventSource | null

export const useFloorMapState = create<FloorMapState>((set, get) => ({
    robots: [],
    robotStates: {},
    floorPlans: [],
    selectedFloorPlan: null,
    selectedRobot: null,

    loadFloorPlan: async () => {
        const [robotResponse, floorPlanResponse] = await Promise.all([
            ElderlyCareApi.robots.getRobots(),
            ElderlyCareApi.floorPlans.getFloorPlans()
        ])

        const floorPlans = floorPlanResponse.floorPlans ?? []
        set({ robots: robotResponse.robots ?? []})
        set({ floorPlans })

        if(floorPlans.length > 0) {
            set({ selectedFloorPlan: floorPlans[0] })
        }
    },

    startLiveUpdate: () => {
        const baseUrl = import.meta.env.VITE_ROBOT_WORKER_URL;
        const eventsUrl = `${baseUrl}/robot-states/live`
    
        eventSource = new EventSource(eventsUrl)
        eventSource.onmessage = (ev) => {
            const newRobotState = JSON.parse(ev.data) as LiveRobotState
            const robotStates = get().robotStates
            const newRobotStates = {...robotStates, [newRobotState.RobotId]: newRobotState.State }
            set({ robotStates: newRobotStates })
        }
    },

    stopLiveUpdate: () => {
        eventSource?.close();
    },

    selectRobot: (robot: Robot | null) => {
        set({ selectedRobot: robot})
    }
}))