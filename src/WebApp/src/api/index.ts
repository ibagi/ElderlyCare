import { Configuration, FloorPlansApi, RobotsApi } from "./generated";

const configuration = new Configuration({
    basePath: import.meta.env.VITE_API_URL
})

export default class ElderlyCareApi {
    static floorPlans = new FloorPlansApi(configuration);
    static robots = new RobotsApi(configuration);
}