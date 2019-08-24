module.exports = {
  root: true,
  parser: '@typescript-eslint/parser',
  "extends": [
    'plugin:@typescript-eslint/recommended',
    'plugin:react/recommended'
  ],
  rules: {
    '@typescript-eslint/indent': [2, 2],
    '@typescript-eslint/explicit-function-return-type': 0,
    'quotes': [2, 'single', { 'avoidEscape': true }]
  },
  parserOptions: {
    ecmaVersion: 2018,
    sourceType: 'module',
    ecmaFeatures: {
      jsx: true
    }
  },
  settings: {
    react: {
      version: 'detect'
    }
  },
  "env": {
    "browser": true,
    "node": true
  }
};
