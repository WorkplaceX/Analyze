const HtmlWebpackPlugin = require('html-webpack-plugin')

module.exports = [{
  mode: 'development',
  context: __dirname + "/src",
  entry: './main2.ts',
  output: {
    filename: 'bundle.js'
  },
  module: {
    rules: [{
      test: /\.html$/,
      use: [{
        loader: 'html-loader'
      }],
    },
    {
      test: /\.tsx?$/,
      use: 'ts-loader',
      exclude: /node_modules/
    },
    {
      test: /\.(png|jpg|gif|ico|css)$/,
      use: [{
        loader: 'file-loader',
        options: {name: '[path][name].[ext]'}
      }]
    }]
  },
  plugins: [
    new HtmlWebpackPlugin({
      template: './index.html'
    })
  ]  
}];