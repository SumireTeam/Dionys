import React from 'react';
import { default as NextApp, Container } from 'next/app';
import CssBaseline from '@material-ui/core/CssBaseline';

class App extends NextApp {
  componentDidMount() {
    const jssStyles = document.querySelector('#jss-server-side');
    if (jssStyles) {
      jssStyles.parentNode.removeChild(jssStyles);
    }
  }

  render() {
    const { Component, pageProps } = this.props;

    return (
      <Container>
        <CssBaseline />
        <Component {...pageProps} />
      </Container>
    );
  }
}

export default App;
