"use strict";

var express = require("express");
var path = require("path");
var bodyParser = require("body-parser");
require('babel-register');
require('./server.js');
var eulerService = require("./euler.service");

var allowCrossDomain = function allowCrossDomain(req, res, next) {
    res.header('Access-Control-Allow-Origin', '*');
    res.header('Access-Control-Allow-Methods', 'GET,PUT,POST,DELETE,OPTIONS');
    res.header('Access-Control-Allow-Headers', 'Content-Type, Authorization, Content-Length, X-Requested-With');

    // intercept OPTIONS method
    if ('OPTIONS' == req.method) {
        if (!res.statusCode) res.sendStatus(200);
    } else {
        next();
    }
};

var app = express();
app.use(express.static(__dirname + "/public"));
app.use(bodyParser.json());
app.use(allowCrossDomain);

if (process) var port = process.env.PORT;
// Initialize the app.
var server = app.listen(port || 8095, function () {
    var port = server.address().port;
    console.log("App now running on port", port);
});

app.get("/euler/:Id", function (req, res) {
    var answer = eulerService.solve(req.params.Id);
    res.status(201).json(answer);
});

app.get("/euler/:Id/:Param", function (req, res) {
    var answer = eulerService.solve(req.params.Id, req.params.Param);
    res.status(201).json(answer);
});
