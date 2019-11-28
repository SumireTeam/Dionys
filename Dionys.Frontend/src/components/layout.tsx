import React, { ReactNode } from "react";
import { Container, Paper, Typography } from "@material-ui/core";
import PropsTypes from "prop-types";

interface ILayoutProps {
    title: string;
    children: ReactNode;
}

const Layout = (props: ILayoutProps) => {
    return (
        <Container maxWidth="lg">
            <Paper className="container">
                <Typography className="container-title" component="h1" variant="h3">
                    {props.title}
                </Typography>

                {props.children}
            </Paper>
        </Container>
    );
};

Layout.propTypes = {
    title: PropsTypes.string.isRequired,
    children: PropsTypes.node
};

export default Layout;
