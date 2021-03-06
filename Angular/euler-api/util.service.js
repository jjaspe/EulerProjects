module.exports.primer = {
    maxIndex:10000,
    primeLists:[],
    generate: function(max){
        var listIndex = 0, index=0;
        this.primeLists[0] = [true, true];
        for (var i = 2; i <= max / 2 + 1; i++) {
            for (var j = i * i; j <= max; j += i) {
                listIndex = Math.floor(j/this.maxIndex);
                index = j%this.maxIndex;
                if(!this.primeLists[listIndex])
                    this.primeLists[listIndex] = [];
                this.primeLists[listIndex][index] = true
            }
        }
        console.log(this.primeLists)
    },
    isPrime: function(n){
        var listIndex = Math.floor(n/this.maxIndex);
        var index= n % this.maxIndex;
        return !this.primeLists[listIndex][index];
    }
}

module.exports.primeGen = function (max) {
    var primes = [true,true];
    for (var i = 2; i <= max / 2 + 1; i++) {
        for (var j = i * i; j <= max; j += i) {
            primes[j] = true;
        }
    }
    return primes;
}

module.exports.isPrime = function (n) {
    var i = 2;
    while(i*i<=n)
        if(n%i++==0)
            return false;
    return true;
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

module.exports.numberOfDigits = function (n) {
    return (""+n).length;
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