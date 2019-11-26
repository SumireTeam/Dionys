import React from "react";
import { Container, Paper, Typography } from "@material-ui/core";
import PropsTypes from "prop-types";

const Layout = ({ title, children }) => {
    return (
        <Container maxWidth="lg">
            <Paper className="container">
                <Typography className="container-title" component="h1" variant="h3">
                    {title}
                </Typography>

                {children}
            </Paper>
        </Container>
    );
};

Layout.propTypes = {
    title: PropsTypes.string.isRequired,
    children: PropsTypes.node
};

export default Layout;
