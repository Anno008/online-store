const webpack = require("webpack");
const CleanWebpackPlugin = require("clean-webpack-plugin");
const HtmlWebpackPlugin = require("html-webpack-plugin");
const UglifyJsPlugin = require("uglifyjs-webpack-plugin");
const path = require("path");

module.exports = ({ production } = {}) => ({
    // entry point of our application, within the `src` directory (which we add to resolve.modules below):
    entry: ["index.jsx"],
    // tell Webpack to load js and jsx files
    resolve: {
        extensions: [".js", ".jsx"],
        modules: ["src", "node_modules"],
    },
    // configure the output directory and publicPath for the devServer

    output: {
        filename: "bundle.js",
        path: path.resolve("dist"),
    },
    // configure the dev server to run 
    devServer: {
        port: 3000,
        historyApiFallback: true,
        inline: true,
    },
    module: {
        rules: [
            {
                enforce: "pre",
                test: /\.jsx?$/,
                exclude: /(node_modules)/,
                loader: "eslint-loader",
            },
            {
                test: /\.jsx?$/,
                include: path.resolve(__dirname, "src"),
                exclude: /node_modules/,
                loader: "babel-loader",
                options: {
                    presets: ["react"],
                    plugins: [
                        "transform-es2015-destructuring",
                        "transform-es2015-parameters",
                        "transform-object-rest-spread",
                    ],
                },
            },
            {
                test: /\.svg?$/,
                include: path.resolve(__dirname, "src"),
                exclude: /node_modules/,
                loader: "svg-url-loader",
                options: {
                    noquotes: true,
                },
            },
            {
                test: /\.css?$/,
                include: path.resolve(__dirname, "src"),
                exclude: /node_modules/,
                use: ["style-loader", "css-loader"],
            },
        ],
    },
    plugins: (production ?
        [new UglifyJsPlugin(), new CleanWebpackPlugin(["dist"])] : []).concat([
            new webpack.DefinePlugin({
                "process.env": {
                    NODE_ENV: JSON.stringify(production ? "production" : "development"),
                },
            }),
            new HtmlWebpackPlugin({
                title: "Frontend-React",
                template: "src/index.html",
            }),
        ]),
});
