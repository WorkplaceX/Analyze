npm cache clean
https://github.com/angular/universal-starter

Webpack
,
  plugins: [
    new webpack.optimize.OccurenceOrderPlugin(),
    new webpack.optimize.UglifyJsPlugin({
    compress: { warnings: false },
    minimize: false,
    mangle: false // Due to https://github.com/angular/angular/issues/6678
	})
  ]


    entry: ["./app/boot-server.js", "./node_modules/angular2-universal/global.js"],

import { bootloader } from 'angular2-universal/dist/node/bootloader';

raw-loader
fs