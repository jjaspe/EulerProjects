var util = require('../util.service');
var data = require('../data.service');
var strint = require('../strint');
var bigN = require('big-number')

export default function (iterations) {
    var nextNum = (p,q) => p.add(bigN(2).multiply(q));
    var nextDen = (p,q) => bigN(-1).multiply(q).add(p);
    var p = bigN(1), q = bigN(1);
    var good = 0; //steps where p has more digits than q

    for(var i = 0; i < iterations ;i++){
        var digitsP = p.toString().length, digitsQ = q.toString().length;
        if (digitsP > digitsQ)
            good++;
        nextNum(p,q);
        q = nextDen(p,q);
        console.log(digitsP);
        console.log(digitsQ)
    }

    return good;        
}