var webpack = require('webpack');

module.exports = {
  entry: ["./app/boot-server.js"],
  output: {
    path: "./dist/",
    filename: "script.min.js"
  },
};