const HtmlWebpackPlugin = require('html-webpack-plugin')

module.exports = [{
  mode: 'development',
  entry: './src/main.js',
  output: {
    filename: 'bundle.js'
  },
  module: {
    rules: [{
      test: /\.html$/,
      use: [ {
        loader: 'html-loader'
      }],
    }]
  },
  plugins: [
    new HtmlWebpackPlugin({
      template: './src/index.html'
    })
  ]  
}];