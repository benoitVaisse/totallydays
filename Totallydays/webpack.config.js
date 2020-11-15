const path = require('path');

module.exports = {
    entry: __dirname + "/wwwroot/source/app.js",
    mode: "development",
    output: {
        path: path.resolve(__dirname, 'wwwroot/dist'),
        filename: "bundle.js"
    },
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
            }
        ]
    },
}