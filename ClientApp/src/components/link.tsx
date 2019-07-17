import React from 'react';
import { Link as RouterLink, LinkProps as RouterLinkProps } from 'react-router-dom';
import { Link as MuiLink } from '@material-ui/core';
import PropTypes from 'prop-types';

/* eslint-disable react/display-name */

const LinkAdapter = React.forwardRef<HTMLAnchorElement, RouterLinkProps>((props, ref) => (
  <RouterLink innerRef={ref} {...props} />
));

const Link = (props) => <MuiLink component={LinkAdapter} to={props.to}>{props.children}</MuiLink>;

Link.propTypes = {
  to: PropTypes.string.isRequired,
  children: PropTypes.node,
};

export default Link;
