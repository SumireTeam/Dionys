import React from 'react';
import { default as NextDocument, Head, Main, NextScript } from 'next/document';
import { ServerStyleSheets } from '@material-ui/styles';

class Document extends NextDocument {
  render() {
    return (
      <html lang="en">
        <Head>
          <meta charSet="utf-8" />

          <meta key="viewport" name="viewport"
            content="initial-scale=1.0, minimum-scale=1.0, width=device-width, shrink-to-fit=no" />

          <link key="font" rel="stylesheet"
            href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap&subset=cyrillic" />

          <link key="icons" rel="stylesheet"
            href="https://fonts.googleapis.com/icon?family=Material+Icons" />

          <style jsx global>{``}</style>
        </Head>

        <body>
          <Main />
          <NextScript />
        </body>
      </html>
    );
  }
}

Document.getInitialProps = async context => {
  const sheets = new ServerStyleSheets();
  const originalRenderPage = context.renderPage;
  context.renderPage = () => originalRenderPage({
    enhanceApp: App => props => sheets.collect(<App {...props} />),
  });

  const initialProps = await NextDocument.getInitialProps(context);
  return {
    ...initialProps,
    styles: [
      (
        <React.Fragment key="styles">
          {initialProps.styles}
          {sheets.getStyleElement()}
        </React.Fragment>
      ),
    ],
  };
};

export default Document;
