import React from "react";
import AppHeader from "./AppHeader";

type Props = {
    children?: any;
}

export default function Layout({ children }: Props) {
    return (
        <React.Fragment>
            <AppHeader/>
            {children}
        </React.Fragment>
    )
}