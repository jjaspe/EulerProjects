module.exports.primeGen = function (max) {
    primes[0] = 1;
    primes[1] = 1;
    for (var i = 2; i <= max / 2 + 1; i++) {
        for (var j = i * i; j <= max; j += i) {
            primes[j] = 1;
        }
    }
    return primes;
}

module.exports.factorial = function (n) {
    if (n < 2)
        return 1;
    else {
        var f = 1;
        for (i = 2; i <= n; i++) {
            f *= i;
        }
        return f;
    }
}

module.exports.continuedFraction = function (d, places) {
    var array=[];
    var frac=0;
    while(array.length<places){
        frac = d%1;
        array.push(d-frac);
        if(frac==0)
            break;
        d=1/frac;        
    }
    return array;
}

module.exports.getDivisors = function (d) {
    var divisors = [];
    for (var i = 2; i < Math.sqrt(d); i++) {
        while (d % i == 0) {
            d = d / i;
            divisors[i] = 1;
        }
    }
    var list = [];
    divisors.forEach((n, i) => {
        if (n == 1)
            list.push(i);
    });
    return list;
}



module.exports.digitize = function (n) {
    var digits = [];
    while (n >= 10) {
        digits.push(n % 10);
        n = (n - n % 10) / 10;
    }
    digits.push(n % 10);
    return digits;
}

module.exports.partitions = function (length) {
    var partiton = function (length) {
        if (length == 1) {
            return ['0', '1'];
        }
        else {
            var previous = partiton(length - 1);
            var next = [];
            previous.forEach(function (element) {
                next.push(element + '0');
                next.push(element + '1');
            });
            return next;
        }
    }
    return partiton(length);
}

module.exports.triangle = function (n) {
    var triangle = [];
    for (var k = 1; k < n; k++) {
        triangle[(k * (k + 1)) / 2] = 1;
    }
    return triangle;
}

module.exports.triangleList = function (n) {
    var pentagon = [];
    for (var k = 1; k < n; k++) {
        pentagon.push((k * (k + 1)) / 2);
    }
    return pentagon;
}

module.exports.pentagon = function (n) {
    var pentagon = [];
    for (var k = 1; k < n; k++) {
        pentagon[(k * (3 * k - 1)) / 2] = 1;
    }
    return pentagon;
}

module.exports.pentagonList = function (n) {
    var pentagon = [];
    for (var k = 1; k < n; k++) {
        pentagon.push((k * (3 * k - 1)) / 2);
    }
    return pentagon;
}

module.exports.hexagon = function (n) {
    var hexagon = [];
    for (var k = 1; k < n; k++) {
        hexagon[(k * (2 * k - 1))] = 1;
    }
    return hexagon;
}