const TerserPlugin = require('terser-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const CssMinimizerPlugin = require('css-minimizer-webpack-plugin');
const CopyPlugin = require('copy-webpack-plugin');
const path = require('path');

module.exports = {
  mode: 'production',
  entry: {
    main: [
      './src/main.js',
    ],
    styles: [
      './src/main.scss',
      './node_modules/boxicons/css/boxicons.css'
    ]
  },
  output: {
    path: path.resolve(__dirname, 'wwwroot/assets/dist'),
    filename: '[name].js',          // fixed names
    chunkFilename: '[name].js',     // ensure vendor/monaco chunks & workers use stable names
    clean: true,
    publicPath: '/_content/Woprkshop.Shared.Ui/assets/dist/',
    chunkLoadingGlobal: 'webpackChunkws_shared_ui'
  },
  module: {
    rules: [
      { test: /\.css$/i, use: [MiniCssExtractPlugin.loader, 'css-loader'] },
      { test: /\.scss$/i, use: [MiniCssExtractPlugin.loader, 'css-loader', 'sass-loader'] },
      { test: /\.(ttf|woff|woff2|eot|svg)$/i, type: 'asset/resource', generator: { filename: 'fonts/[name][ext]' } },
      { test: /\.(png|jpg|jpeg|gif)$/i, type: 'asset/resource', generator: { filename: 'images/[name][ext]' } }
    ]
  },
  plugins: [
    new MiniCssExtractPlugin({
      filename: '[name].css',          // fixed css names
      chunkFilename: '[name].css'
    }),
    new CopyPlugin({
      patterns: [
        { from: 'src/main.js', to: '' }
      ]
    })
  ],
  optimization: {
    minimize: true,
    minimizer: [
      new TerserPlugin({
        terserOptions: {
          format: { comments: false },
          keep_fnames: /^(blazorHelpers|downloadFile|window\..*)$/,
        },
        extractComments: false
      }),
      new CssMinimizerPlugin()
    ]
  },
  performance: {
    maxEntrypointSize: 600 * 1024,
    maxAssetSize: 400 * 1024
  }
};