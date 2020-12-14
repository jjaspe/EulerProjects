var util = require('../util.service');
var data = require('../data.service');
var strint = require('../strint');
var bigN = require('big-number');

var primes;

var checkPrime = (n) => util.primer.isPrime(n)

export default function (max) {
    var maxPrime = max*max+27;        
    var i = 0, n=10, sum = 0, n2;
    var found = [];
    
    util.primer.generate(maxPrime);

    while(n < max){
        n2 = n*n;
        if(checkPrime(n2+1) && checkPrime(n2+3) && checkPrime(n2+7) && checkPrime(n2+9)
            && checkPrime(n2+13) && checkPrime(n2+27))
            {
                sum+=n;
                found.push(n);
            }
        var inc = 0, i4=i%3;
        switch(i4)
        {
            case 0: inc = 120;break;
            case 1: inc = 70; break;
            case 2: inc = 20; break;
        }

        n = n + inc;
        i = (i+1)%3;
    }
    console.log(found);
    return sum;
}