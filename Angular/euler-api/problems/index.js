var util = require('../util.service');
var data = require('../data.service');
var strint = require('../strint');
var primes = [];


import problem313 from './problem313'
import problem57 from './problem57'


    var problem314 = function () {
        var ticks = 10;
        var length = function (p1, p2) {
            return Math.sqrt(Math.pow(p1[0] - p2[0], 2) + Math.pow(p1[1] - p2[1], 2));
        }

        var triangleArea = function (p1, p2) {
            return Math.abs((p1[0] - p2[0]) * (p1[1] - p2[1]) / 2);
        }

    }

    var problem33 = function () {
        var reduce = function (n, d) {
            var reductions = [];
            for (var i = 2; i <= n; i++) {
                if (n % i == 0 && d % i == 0) {
                    reductions.push({ 0: n / i, 1: d / i });
                }
            }
            return reductions;
        }

        var found = [];

        for (var ni = 1; ni < 10; ni++) {
            for (var nj = 1; nj < 10; nj++) {
                for (var mi = ni; mi < 10; mi++) {
                    for (var mj = mi == ni ? mj + 1 : 1; mj < 10; mj++) {
                        var num = ni * 10 + nj, den = mi * 10 + mj;
                        reductions = reduce(num, den);
                        //console.log("n:"+num+", den:"+den);
                        //console.log("reductions:"+reductions[0]);
                        var shared = {};
                        if (ni == mi)
                            shared = { 0: nj, 1: mj };
                        else if (ni == mj)
                            shared = { 0: nj, 1: mi };
                        else if (nj == mi)
                            shared = { 0: ni, 1: mj };
                        else if (nj == mj)
                            shared = { 0: ni, 1: mi }
                        if (shared) {
                            shared[2] = num;
                            shared[3] = den;
                            for (var i = 0; i < reductions.length; i++) {
                                if (shared[0] == reductions[i][0] && shared[1] == reductions[i][1])
                                    found.push(shared);
                            }
                        }
                    }
                }
            }
        }

        return found;
    }

    var problem34 = function () {
        var factorialArray = [];

        var sumOfDigitFactorials = function (n) {
            var digits = util.digitize(n);
            return digits.reduce((t, n) => { return factorialArray[n] + t }, 0);
        }

        for (var i = 0; i < 10; i++) {
            factorialArray[i] = util.factorial(i);
        }

        var sum = 0;
        for (var i = 3; i < 10000000; i++) {
            if (sumOfDigitFactorials(i) == i)
                sum += i;
        }
        return sum;
    }

    var problem38 = function () {
        var isPandigital = function (n) {
            var digits = util.digitize(n);
            for (var i = 1; i <= 9; i++) {
                if (digits.indexOf(i) < 0)
                    return false;
            }
            if (digits.length > 9)
                return false;
            return true;
        }
        var multipliers = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        var biggest = 0;
        var current = "";
        for (var i = 1; i < 10000; i++) {
            current = "";
            for (var j = 0; j < 9; j++) {
                current = current + (multipliers[j] * i + "");
                if (current.length == 9) {
                    total = current * 1;

                    if (isPandigital(total) && total > biggest) {
                        console.log(total);
                        biggest = total;
                    }

                } else if (current.length > 9)
                    break;
            }
        }
        return biggest;
    }

    var problem39 = function () {
        var perims = [];
        for (var c = 998; c > 0; c--) {
            for (var b = 1; b < c && c + b < 1000; b++) {
                for (var a = c - b; a < c && a <= b && a + b + c <= 1000; a++) {
                    var perim = a + b + c;
                    if (a * a + b * b === c * c)
                        perims[perim] = perims[perim] ? perims[perim] + 1 : 1;
                }
            }
        }
        var max = perims.reduce((p, c) => Math.max(p, c));
        return perims.indexOf(max);
    }

    var problem40 = function (value) {
        var threshold = function (n) {
            return n * Math.pow(10, n) - (Math.pow(10, n) - 1) / 9;
        }

        var getDigit = function (n) {
            var last = 0, lastThreshold = 0;
            for (var i = 0; i < 7; i++) {
                if (n < threshold(i)) {
                    last = i - 1;
                    break;
                }
                lastThreshold = threshold(i);
            }
            var leftOver = n - lastThreshold;
            var mod = leftOver % (last + 1);
            var numbers = (leftOver - mod) / (last + 1);
            var originNumber = Math.pow(10, last) - 1 + numbers;
            if (mod > 0)
                originNumber++;
            var digits = util.digitize(originNumber).reverse();
            var index = (mod - 1) % (last + 1);
            return digits[index];
        }

        var prod = 1;
        for (var i = 0; i < 7; i++) {
            prod *= getDigit(Math.pow(10, i));
        }
        return prod;
    }

    var problem41 = function (max) {
        var isPandigital = function (n) {
            var digits = util.digitize(n);
            for (var i = 1; i <= digits.length; i++) {
                if (digits.indexOf(i) < 0)
                    return false;
            }
            return true;
        }

        var primes = util.primeGen(max);
        for (var i = primes.length - 1; i > 0; i--) {
            if (!primes[i] && isPandigital(i))
                return i;
        }
    }

    var problem42 = function () {
        var isTriangle = function (n) {
            var val = (-1 + Math.sqrt(1 + 8 * n)) / 2;
            return val % 1 == 0;
        }

        var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        console.log(letters[0]);
        var words = data.problem42Words;
        var triangle = util.triangle(500);
        var count = 0;
        return letters.indexOf("M") + letters.indexOf("Y") + letters.indexOf("A") + 2;/*
        for(var i=0;i<words.length;i++){
            var sum=0;
            for(var j=0;words[i][j];j++){
                sum+=letters.indexOf(words[i][j])+1;                
            }
            
            if(i<10)
            {
                console.log("sum:"+sum+",word:"+words[i]);
            }
            if(isTriangle(sum)){
                console.log("count:"+count+",sum:"+sum+",word:"+words[i]);
                count++;
                //console.log("WORD:"+words[i]);
                //console.log("SUM:"+sum);
            }
                
        }
        return count;*/
    }

    var problem43 = function (max) {
        //1406357289
        var getDigitCombinations = function (number, digit, isZero) {
            var combos = [];
            for (var i = isZero ? 1 : 0; number[i]; i++) {
                var parts = [number.substring(0, i), number.substring(i)];
                combos.push((parts[0] + digit + parts[1]));
            }
            combos.push((number + (digit + "")));
            return combos;
        }

        var buildPandigitals = function (max) {
            if (max == 1)
                return ["1"];
            else {
                var previous = buildPandigitals(max - 1);
                var next = [];
                for (var i = 0; previous[i]; i++) {
                    getDigitCombinations(previous[i], max, false).forEach(n => next.push(n));
                }
                return next;
            }

        }

        var primesPartialDivisibility = function (n, listOfPrimes) {
            for (var i = 0; i < 7; i++) {
                var piece = (n + "").slice(2 + i - 1, 2 + i + 2) * 1;
                if (piece % listOfPrimes[i] != 0)
                    return false;
            }
            return true;
        }

        var count = 0;
        var primes = [];
        var primesBool = util.primeGen(18);
        for (var i = 0; i < primesBool.length; i++) {
            if (primesBool[i] != 1)
                primes.push(i);
        }
        var ninePan = buildPandigitals(9);
        var next = [];
        ninePan.forEach(n => getDigitCombinations(n, 0, true).forEach(m => next.push(m * 1)));
        next.forEach(n => {
            if (primesPartialDivisibility(n, primes)) {
                count += n;
                console.log(n);
            }
        });
        return count;
    }

    var problem45 = function (max) {
        var isTriangle = function (n) {
            var val = (-1 + Math.sqrt(1 + 8 * n)) / 2;
            return val % 1 == 0;
        }

        var isHexagon = function (n) {
            var val = (1 + Math.sqrt(1 + 8 * n)) / 4;
            return (val % 1 == 0);
        }

        var pentagons = util.pentagonList(max);
        for (var i = 0; pentagons[i]; i++) {
            if (pentagons[i] > 40755 && isTriangle(pentagons[i]) && isHexagon(pentagons[i])) {
                console.log(pentagons[i]);
                return pentagons[i];
            }
        }
        return "not found";
    }

    var problem46 = function (max) {
        var primes = util.primeGen(max);
        for (var i = 9; i < primes.length; i += 2) {
            if (primes[i] == 1) {//Composite
                var maxSquare = Math.sqrt(i / 2) | 1;
                var found = true;
                for (var j = 1; j <= maxSquare; j++) {
                    if (!primes[i - 2 * j * j])//Check if i-2*m^2 is a prime, if it is for any, conjecture is true for that one so break.
                    {
                        //console.log(i+" = 2*" + j +"^2 + " + (i-2*j*j))
                        break;
                    }
                }
                if (j == maxSquare + 1)// got to end, no match
                    return i;
            }
        }
        return "try larger";
    }

    var problem47 = function (max) {
        var number = 4;
        var primes = util.primeGen(max);
        var primeFactorCount = function (n) {
            var count = 0;
            var m = n;
            for (var i = 2; m > 1; i++) {
                if (m % i == 0) {
                    count++;
                    while (m % i == 0)
                        m /= i;
                }
            }
            return count;
        }

        for (var i = 1; i < max; i++) {
            for (var j = 0; j < number; j++) {
                if (primeFactorCount(i + j) != number) {
                    i = i + j;
                    break;
                }
            }
            if (j == number)
                return i;
        }
        return "try larger";
    }

    var problem48 = function (max) {
        var digits = 10;
        var cutDown = function (n) {
            return n % 10000000000
        }
        var sum = 0;

        for (var i = 1; i <= max; i++) {
            var current = 1;
            for (var j = 0; j < i; j++) {
                current = cutDown(current * i);
            }
            sum += cutDown(current);
        }
        return sum;
    }

    //32456,8,"11011" => 88488
    //Loop through n, digitize n, generate partition array, for each partition replace digits at 1 with same digit
    //Check if 8 primes
    var problem51 = function (max) {
        var replaceDigits = function (orig, rep, partition) {
            var stringValue = "" + orig;
            var newValue = "";
            for (var i = 0; i < partition.length; i++) {
                if (partition[i] == '1') {
                    newValue += '' + rep;
                }
                else
                    newValue += stringValue[i];
            }
            return newValue * 1;
        }

        var getMaxCount = function (n, primes, printStuff) {
            n = "" + n;
            var partitions = util.partitions(n.length);
            var maxCount = 0;
            partitions.splice(0, 1); //delete first as it changes nothing
            for (var j = 0; j < partitions.length; j++) {
                for (var k = 0, primeCount = 0; k < 10 && k - primeCount < 3; k++) {
                    var newNumber = replaceDigits(n, k, partitions[j]);
                    if ((newNumber + "").length < n.length)
                        continue;
                    if (!primes[newNumber]) {
                        primeCount++;
                        if (printStuff)
                            console.log("new:" + newNumber + ", partition:" + partitions[j]);
                    }
                }
                if (primeCount > maxCount) {
                    maxCount = primeCount;
                    if (printStuff)
                        console.log("n:" + n + ", partition:" + partitions[j] + ",primeCount:" + primeCount);
                }
            }
            return maxCount;
        }

        var primes = util.primeGen(max * 10);
        var maxCount = 0, currentCount;
        for (var i = 100; i < max; i++) {
            currentCount = getMaxCount(i + "", primes, false);
            if (currentCount > maxCount) {
                console.log("New Max:" + currentCount);
                getMaxCount(i + "", primes, true);
                maxCount = currentCount;
            }
            if (maxCount == 8) {
                return 'max at:' + i;
            }
        }
    }

    var problem66 = function (max) {
        var findMinSolution = function (d) {
            var coeffs = util.continuedFraction(Math.sqrt(d),100);
            var hpp="0",hp="1",kpp="1",kp="0";
            for(var i=0;i<coeffs.length;i++){
                newArray=getConvergent(coeffs[i],hp,hpp,kp,kpp);
                x=newArray[0]+"";
                y=newArray[1]+"";
                if(!strint.eq(strint.sub(strint.mul(x,x),strint.mul(d+"",strint.mul(y,y))),"1")){
                    hpp=hp;
                    kpp=kp;
                    hp=x;
                    kp=y;
                }
                else{
                    if( d == 61)
                    console.log(x);
                    return x;
                } 
            }
            if( d == 61)
                console.log(x);
        }
        
        var getConvergent = function(a,h1,h2,k1,k2){
            return [a*h1+h2,a*k1+k2];
        }

        var highestX = "0", highestD = "0";
        for (var d = 1; d <= max; d++) {
            
            if (Math.sqrt(d) % 1 == 0)
                continue;
            current = findMinSolution(d);
            if ( st.gt(current , highestX)) {
                highestX = current;
                highestD = d;
                console.log("D:" + d +", highestD:"+highestD + ",highestX:"+highestX);
            }
        }
        return highestD;
    }

export default {
    problem313,
    problem57
};