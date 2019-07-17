import React from 'react';
import { Link as RouterLink } from 'react-router-dom';
import PropTypes from 'prop-types';

/*import PropTypes from 'prop-types';
import clsx from 'clsx';
import MuiLink from '@material-ui/core/Link';

const NextComposed = React.forwardRef((props: any, ref: any) => {
  const { href, prefetch, as, ...other } = props;

  return (
    <a ref={ref} {...other} />
  );
}) as any;

NextComposed.propTypes = {
  href: PropTypes.string,
  as: PropTypes.string,
  prefetch: PropTypes.bool,
};

const Link = (props) => {
  const {
    activeClassName = 'active',
    router,
    className: classNameProps,
    innerRef,
    naked,
    ...other
  } = props;

  const className = clsx(classNameProps, {
    [activeClassName]: router.pathname === props.href && activeClassName,
  });

  if (naked) {
    return <NextComposed className={className} ref={innerRef} {...other} />;
  }

  return <MuiLink component={NextComposed} className={className} ref={innerRef} {...other} />;
}

Link.propTypes = {
  activeClassName: PropTypes.string,
  as: PropTypes.string,
  className: PropTypes.string,
  href: PropTypes.string,
  innerRef: PropTypes.oneOfType([PropTypes.func, PropTypes.object]),
  naked: PropTypes.bool,
  onClick: PropTypes.func,
  prefetch: PropTypes.bool,
  router: PropTypes.shape({
    pathname: PropTypes.string.isRequired,
  }).isRequired,
};

const RouterLink = withRouter(Link);

export default React.forwardRef((props, ref) => <RouterLink {...props} innerRef={ref} />) as any;*/

const Link = (props) => {
  return <RouterLink to={props.to}>{props.children}</RouterLink>;
};

Link.propTypes = {
  to: PropTypes.string.isRequired,
  children: PropTypes.node,
};

export default Link;
