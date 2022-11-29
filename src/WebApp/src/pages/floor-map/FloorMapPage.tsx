import FloorMap from "./FloorMap"
import FloorMapDetails from "./FloorMapDetails"
import FloorMapSidebar from "./FloorMapSidebar"

export default function FloorMapPage() {
    return (
        <section className="flex bg-gray-100">
            <FloorMapSidebar/>
            <FloorMap/>
            <FloorMapDetails />
        </section>
    )
}