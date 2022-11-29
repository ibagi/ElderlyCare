import { Layer, Image } from "react-konva"
import useImage from "use-image";
import { useFloorMapState } from "../FloorMapState";

const BASE_IMAGE_URL = import.meta.env.VITE_API_URL

interface Props {
    svgUrl: string;
}

function SvgImage({ svgUrl }: Props) {
    const [img] = useImage(svgUrl);
    return <Image image={img} />;
}

export default function BaseLayer() {
    const selectedFloorPlan = useFloorMapState(state => state.selectedFloorPlan)
    if (!selectedFloorPlan) {
        return null;
    }

    const url = `${BASE_IMAGE_URL}${selectedFloorPlan.svgUrl}`

    return (
        <Layer >
            <SvgImage svgUrl={url} />
        </Layer>
    )
}