import React from 'react';
import { Link as MuiLink } from '@material-ui/core';
import PropTypes from 'prop-types';
import LinkAdapter from './link-adapter';

const Link = (props) => <MuiLink component={LinkAdapter} to={props.to}>{props.children}</MuiLink>;

Link.propTypes = {
  to: PropTypes.string.isRequired,
  children: PropTypes.node,
};

export default Link;
