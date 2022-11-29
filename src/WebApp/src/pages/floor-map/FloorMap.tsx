import React, { useEffect, useRef } from "react";
import { Stage } from "react-konva";
import { useFloorMapState } from "./FloorMapState";
import { mapZoom, useMapStateSave } from "./FloorMapFunctions";

import BaseLayer from "./layers/BaseLayer";
import RobotsLayer from "./layers/RobotsLayer";

export default function FloorMap() {
    const stageRef = useRef()
    const stageContainerRef = useRef()
    const handleZoom = mapZoom(stageRef.current)

    useMapStateSave(stageRef)

    const loadFloorPlan = useFloorMapState(state => state.loadFloorPlan);
    const startLiveUpdate = useFloorMapState(state => state.startLiveUpdate);
    const stopLiveUpdate = useFloorMapState(state => state.stopLiveUpdate);

    useEffect(() => {
        loadFloorPlan()
    }, [])

    useEffect(() => {
        startLiveUpdate()
        return stopLiveUpdate
    }, [])

    return (
        <section ref={stageContainerRef.current}>
            <Stage
                ref={stageRef.current}
                draggable={true}
                onWheel={handleZoom}
                width={window.innerWidth}
                height={window.innerHeight}>
                <BaseLayer />
                <RobotsLayer />
            </Stage>
        </section>
    )
}