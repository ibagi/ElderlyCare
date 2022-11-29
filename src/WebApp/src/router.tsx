import { createBrowserRouter } from "react-router-dom";
import FloorMapPage from "./pages/floor-map/FloorMapPage";

const router = createBrowserRouter([
    {
        path: "/",
        element: <FloorMapPage />,
    },
]);

export default router;