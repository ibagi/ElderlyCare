import { List, ListItem, ListItemButton, ListItemIcon, ListItemText, Paper, Typography } from "@mui/material";
import i18n from "../../i18n";
import { useFloorMapState } from "./FloorMapState";

import styles from './FloorMapSidebar.module.css';
import { Circle } from "@mui/icons-material";

export default function FloorMapSidebar() {
    const robots = useFloorMapState(state => state.robots)
    const selectedFloorPlan = useFloorMapState(state => state.selectedFloorPlan)
    const selectRobot = useFloorMapState(state => state.selectRobot)

    return (
        <Paper elevation={1} className={styles.container}>
            <Typography variant="subtitle1" className="pb-5">
                {selectedFloorPlan?.name ?? ''}
            </Typography>
            <Typography variant="subtitle2">
                {i18n.t('pages.floorMap.robots')}
            </Typography>
            <List dense={true}>
                {robots.map(r => (
                    <ListItem disablePadding key={r.robotId} onClick={() => selectRobot(r)}>
                        <ListItemButton>
                            <ListItemIcon>
                                <Circle htmlColor="#00b572" />
                            </ListItemIcon>
                            <ListItemText primary={r.robotName ?? ''} secondary={r.state?.Status} />
                        </ListItemButton>
                    </ListItem>))}
            </List>
        </Paper>
    )
}