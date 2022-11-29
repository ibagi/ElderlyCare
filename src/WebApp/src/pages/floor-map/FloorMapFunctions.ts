import Konva from 'konva';
import { useEffect } from "react";

export const getMousePosition = (stage: any) => {
    const scale = stage.scaleX();
    const position = stage.getPointerPosition()
    return {
        x: position.x / scale - stage.x() / scale,
        y: position.y / scale - stage.y() / scale
    };
}

export const getPosition = (stage: any, x: any, y: any) => {
    const scale = stage.scaleX();
    return {
        x: x / scale - stage.x() / scale,
        y: y / scale - stage.y() / scale
    };
}

export const mapZoom = (stage: any) => (e: any) => {
    if (!stage) {
        return
    }

    e.evt.preventDefault();

    const scaleBy = 1.1;
    const oldScale = stage.scaleX();

    const mousePointTo = {
        x: stage.getPointerPosition().x / oldScale - stage.x() / oldScale,
        y: stage.getPointerPosition().y / oldScale - stage.y() / oldScale
    };

    const newScale = e.evt.deltaY > 0
        ? oldScale * scaleBy
        : oldScale / scaleBy;

    stage.scale({ x: newScale, y: newScale });

    const newPos = {
        x: -(mousePointTo.x - stage.getPointerPosition().x / newScale) * newScale,
        y: -(mousePointTo.y - stage.getPointerPosition().y / newScale) * newScale
    };

    saveMapState(newPos, newScale)
    stage.position(newPos);
    stage.batchDraw();
}

export function useMapStateSave(stageRef: any) {
    useEffect(() => {
        const stage = stageRef.current
        if (!stage) {
            return
        }

        restoreMapState(stage)

        stage.on('mouseup dragmove', () => {
            if (stage) {
                const scale = stage.scaleX()
                const mousePointTo = {
                    x: stage.getPointerPosition().x / scale - stage.x() / scale,
                    y: stage.getPointerPosition().y / scale - stage.y() / scale
                };
                const position = {
                    x: -(mousePointTo.x - stage.getPointerPosition().x / scale) * scale,
                    y: -(mousePointTo.y - stage.getPointerPosition().y / scale) * scale
                };

                saveMapState(position, scale)
            }
        })
    }, [stageRef])
}

function saveMapState(position: any, scale: any) {
    window.localStorage.setItem('MAP_POSITION', JSON.stringify(position))
    window.localStorage.setItem('MAP_SCALE', JSON.stringify(scale))
}

export function restoreMapState(stage: any) {
    const position = window.localStorage.getItem('MAP_POSITION')
    const scale = window.localStorage.getItem('MAP_SCALE')

    if (position && scale) {
        const scaleValue = JSON.parse(scale)
        stage.scale({ x: scaleValue, y: scaleValue });
        stage.position(JSON.parse(position))
    }
}

const GUIDELINE_OFFSET = 5;

export function useMapSnap(layer: any, stage: any) {
    useEffect(init, [layer, stage])

    function getLineGuideStops(skipShape: any) {
        // we can snap to stage borders and the center of the stage
        const vertical = [0, stage.width() / 2, stage.width()];
        const horizontal = [0, stage.height() / 2, stage.height()];

        // and we snap over edges and center of each object on the canvas
        stage.find('.object').forEach((guideItem: any) => {
            if (guideItem === skipShape) {
                return;
            }
            const box = guideItem.getClientRect();
            // and we can snap to all edges of shapes
            vertical.push([box.x, box.x + box.width, box.x + box.width / 2]);
            horizontal.push([box.y, box.y + box.height, box.y + box.height / 2]);
        });
        return {
            vertical: vertical.flat(),
            horizontal: horizontal.flat(),
        };
    }

    function getObjectSnappingEdges(node: any) {
        const box = node.getClientRect();
        return {
            vertical: [
                {
                    guide: Math.round(box.x),
                    offset: Math.round(node.x() - box.x),
                    snap: 'start',
                },
                {
                    guide: Math.round(box.x + box.width / 2),
                    offset: Math.round(node.x() - box.x - box.width / 2),
                    snap: 'center',
                },
                {
                    guide: Math.round(box.x + box.width),
                    offset: Math.round(node.x() - box.x - box.width),
                    snap: 'end',
                },
            ],
            horizontal: [
                {
                    guide: Math.round(box.y),
                    offset: Math.round(node.y() - box.y),
                    snap: 'start',
                },
                {
                    guide: Math.round(box.y + box.height / 2),
                    offset: Math.round(node.y() - box.y - box.height / 2),
                    snap: 'center',
                },
                {
                    guide: Math.round(box.y + box.height),
                    offset: Math.round(node.y() - box.y - box.height),
                    snap: 'end',
                },
            ],
        };
    }

    function getGuides(lineGuideStops: any, itemBounds: any) {
        const resultV: any = [];
        const resultH: any = [];

        lineGuideStops.vertical.forEach((lineGuide: any) => {
            itemBounds.vertical.forEach((itemBound: any) => {
                const diff = Math.abs(lineGuide - itemBound.guide);
                // if the distance between guild line and object snap point is close we can consider this for snapping
                if (diff < GUIDELINE_OFFSET) {
                    resultV.push({
                        lineGuide: lineGuide,
                        diff: diff,
                        snap: itemBound.snap,
                        offset: itemBound.offset,
                    });
                }
            });
        });

        lineGuideStops.horizontal.forEach((lineGuide: any) => {
            itemBounds.horizontal.forEach((itemBound: any) => {
                const diff = Math.abs(lineGuide - itemBound.guide);
                if (diff < GUIDELINE_OFFSET) {
                    resultH.push({
                        lineGuide: lineGuide,
                        diff: diff,
                        snap: itemBound.snap,
                        offset: itemBound.offset,
                    });
                }
            });
        });

        const guides = [];

        // find closest snap
        const minV = resultV.sort((a: any, b: any) => a.diff - b.diff)[0];
        const minH = resultH.sort((a: any, b: any) => a.diff - b.diff)[0];
        if (minV) {
            guides.push({
                lineGuide: minV.lineGuide,
                offset: minV.offset,
                orientation: 'V',
                snap: minV.snap,
            });
        }
        if (minH) {
            guides.push({
                lineGuide: minH.lineGuide,
                offset: minH.offset,
                orientation: 'H',
                snap: minH.snap,
            });
        }
        return guides;
    }

    function drawGuides(guides: any) {
        guides.forEach((lg: any) => {
            if (lg.orientation === 'H') {
                const line = new Konva.Line({
                    points: [-6000, lg.lineGuide, 6000, lg.lineGuide],
                    stroke: 'rgb(0, 161, 255)',
                    strokeWidth: 1,
                    name: 'guid-line',
                    dash: [4, 6],
                });
                layer.add(line);
                layer.batchDraw();
            } else if (lg.orientation === 'V') {
                const line = new Konva.Line({
                    points: [lg.lineGuide, -6000, lg.lineGuide, 6000],
                    stroke: 'rgb(0, 161, 255)',
                    strokeWidth: 1,
                    name: 'guid-line',
                    dash: [4, 6],
                });
                layer.add(line);
                layer.batchDraw();
            }
        });
    }

    function init() {
        if (!layer || !stage) {
            return
        }

        layer.on('dragmove', function (e: any) {
            // clear all previous lines on the screen
            layer.find('.guid-line').destroy();

            // find possible snapping lines
            const lineGuideStops = getLineGuideStops(e.target);
            // find snapping points of current object
            const itemBounds = getObjectSnappingEdges(e.target);

            // now find where can we snap current object
            const guides = getGuides(lineGuideStops, itemBounds);

            // do nothing of no snapping
            if (!guides.length) {
                return;
            }

            drawGuides(guides);

            // now force object position
            guides.forEach((lg) => {
                switch (lg.snap) {
                    case 'start': {
                        switch (lg.orientation) {
                            case 'V': {
                                e.target.x(lg.lineGuide + lg.offset);
                                break;
                            }
                            case 'H': {
                                e.target.y(lg.lineGuide + lg.offset);
                                break;
                            }
                            default:
                                return;
                        }
                        break;
                    }
                    case 'center': {
                        switch (lg.orientation) {
                            case 'V': {
                                e.target.x(lg.lineGuide + lg.offset);
                                break;
                            }
                            case 'H': {
                                e.target.y(lg.lineGuide + lg.offset);
                                break;
                            }
                            default:
                                return;
                        }
                        break;
                    }
                    case 'end': {
                        switch (lg.orientation) {
                            case 'V': {
                                e.target.x(lg.lineGuide + lg.offset);
                                break;
                            }
                            case 'H': {
                                e.target.y(lg.lineGuide + lg.offset);
                                break;
                            }
                            default:
                                return;
                        }
                        break;
                    }
                    default:
                        return;
                }
            });
        });

        layer.on('dragend', function (e: any) {
            // clear all previous lines on the screen
            layer.find('.guid-line').destroy();
            layer.batchDraw();
        });
    }
}
