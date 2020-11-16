const path = require('path');

module.exports = (env, agrs) => ({
    entry: __dirname + "/wwwroot/source/app.js",
    mode: agrs.mode || "development",
    output: {
        path: path.resolve(__dirname, 'wwwroot/dist'),
        filename: "bundle.js"
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
            }
        ]
    },
})