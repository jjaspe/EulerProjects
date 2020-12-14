var util = require('../util.service');
var data = require('../data.service');
var strint = require('../strint');

export default function () {
        problem313NewAlgo = function (ps) {
            var x = ps - 9;
            modX6 = x % 6;
            if (modX6 == 0) {
                var m = (x + 6) / 6;
                return m / 4 | 1;//Math.floor((m)/4)+1;
            }
            else if (modX6 == 4) {
                var m = (x + 2) / 6;
                return Math.floor((m - 2) / 4) + 1;
            }
        }
        var max = 999984;
        var maxS = max * max;
        primes = util.primeGen(max);
        var count = 0;

        for (var i = 3; i < primes.length; i++) {
            if (!primes[i]) {
                count += 2 * problem313NewAlgo(i * i);
            }
        }
        return "real:" + count;
    }