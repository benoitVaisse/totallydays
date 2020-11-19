const path = require('path');
const ExtractTextPluguin = require("extract-text-webpack-plugin");
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

let extractSass = new ExtractTextPluguin({
    filename:"dist.css"
})
let entryFile = { "bundlejs": __dirname + "/wwwroot/source/js/app.js", "bundlecss": __dirname + "/wwwroot/source/css/app.scss"}

module.exports = (env, agrs) => ({
    entry: entryFile,
    mode: agrs.mode || "development",
    output: {
        path: path.resolve(__dirname, 'wwwroot/dist'),
        filename: "[name].js"
    },
    devtool:"source-map",
    target: ['web', 'es5'],
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /(node_modules|bower_components)/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: [
                            ['@babel/preset-env', {
                                targets: {
                                    "ie": "11"
                                }
                            }]
                        ]
                    }
                }
            },
            {
                test: /\.scss$/,
                use: [
                    {
                        loader : "style-loader",
                    },
                    MiniCssExtractPlugin.loader,
                    {
                        loader: 'css-loader'
                    },
                    {
                        loader: 'postcss-loader',
                        options: {
                            sourceMap: true
                        }
                    },
                    {
                        loader: 'sass-loader'
                    }
                ]
            }
            
        ]
    },
    plugins: [
        new MiniCssExtractPlugin({
            filename : "[name].css"
        })
    ]
})