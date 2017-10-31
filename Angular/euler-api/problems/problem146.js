var util = require('../util.service');
var data = require('../data.service');
var strint = require('../strint');
var bigN = require('big-number');


export default function (max) {
    var maxPrime = max*max+27;        
    var i = 0, n=10, sum = 0, n2;
    var found = [];
    

    while(n < max){
        n2 = n*n;
        if(util.isPrime(n2+1) && util.isPrime(n2+3) && util.isPrime(n2+7) && util.isPrime(n2+9)
            && util.isPrime(n2+13) && util.isPrime(n2+27))
            {
                sum+=n;
                found.push(n);
            }
        var inc = 0, i4=i%4;
        if(i4 == 0 )
            inc = 15;
        else if (i4 == 1)
            inc = 70;
        else 
            inc = 20;

        n = n + inc;
        console.log("checking:"+n)
        i = (i+1)%3;
    }
    console.log(found);
    return sum;
}