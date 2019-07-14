import React from 'react';
import Head from 'next/head';
import {
  Container,
  Paper,
  Typography,
  Theme,
  makeStyles,
  createStyles,
} from '@material-ui/core';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      padding: theme.spacing(2, 2),
      margin: theme.spacing(2, 0),
    },
    header: {
      margin: theme.spacing(0, 0, 2, 0),
    },
  }),
);

const Layout = ({ title, children }) => {
  const classes = useStyles({});

  return (
    <Container maxWidth="lg">
      <Head>
        <title>{title}</title>
      </Head>

      <Paper className={classes.root}>
        <Typography className={classes.header} component="h1" variant="h3">{title}</Typography>

        {children}
      </Paper>
    </Container>
  );
};

export default Layout;
