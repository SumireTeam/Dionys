import React from "react";
import { Link as MuiLink } from "@material-ui/core";
import PropTypes from "prop-types";
import LinkAdapter from "./link-adapter";

interface ILinkProps {
    children: any;
    to: any;
}

const Link = (props: ILinkProps) => (
    <MuiLink component={LinkAdapter} to={props.to}>
        {props.children}
    </MuiLink>
);

Link.propTypes = {
    to: PropTypes.string.isRequired,
    children: PropTypes.node
};

export default Link;
