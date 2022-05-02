# The primes 3, 7, 109, and 673, are quite remarkable. 
# By taking any two primes and concatenating them in any order the result will always be prime. 
# For example, taking 7 and 109, both 7109 and 1097 are prime. The sum of these four primes, 792, 
# represents the lowest sum for a set of four primes with this property.
# Find the lowest sum for a set of five primes for which any two primes concatenate to produce another prime.

# Get all primes up to n (say 10000)
# Get all combination of 5 primes (use partitions or push pop recursion)
# For each combination, check if all concats are prime
# Do it in order so first one found is smallest

from pydantic import conint
from utils import is_prime_using_sieve, sieve
import math

max_primes = 1000000
sieve_limit = 100000
prime_limit = 10000
primes = sieve(sieve_limit)
# set 2 and 5 to false since concating them will never result in prime
primes_true = [i for i in range(prime_limit) if primes[i] and i != 2 and i != 5]
saved = [[True for i in range(prime_limit)] for j in range(prime_limit)]

def is_prime(x):
    if x < sieve_limit:
        return primes[x] == 1
    else:
        return is_prime_using_sieve(x, primes)

def are_concats_prime(prime1, prime2):
    added1 = prime1 * (10**int(math.log10(prime2)+1)) + prime2
    added2 = prime2 * (10**int(math.log10(prime1)+1)) + prime1
    concat_prime = is_prime(added1) and is_prime(added2)
    if not concat_prime:
        saved[prime1][prime2] = False
        saved[prime2][prime1] = False
    return concat_prime

def check_concats(prime_comb):
    indeces = range(len(prime_comb))
    pairs = [[prime_comb[i], prime_comb[j]] for i in indeces for j in indeces if i < j]
    for pair in pairs:
        concats_prime = are_concats_prime(pair[0], pair[1])
        if not concats_prime:
            return False
    return True

def check_combs():
    for i in primes_true:
        for j in [n for n in primes_true if n > i and saved[i][n]]:
            for k in [n for n in primes_true if n > j and saved[i][n] and saved[j][n]]:
                for m in [n for n in primes_true if n > k and saved[i][n] and saved[j][n] and saved[k][n]]:
                    for p in [n for n in primes_true if n > m \
                        and saved[i][n] and saved[j][n] and saved[j][n] and saved[k][n] and saved[m][n]]:
                        comb = [i, j, k, m, p]
                    # comb = [i, j, k, m]
                        if check_concats(comb):
                            return comb
    return None

def check_combinations(ranks):
    if ranks == 2:
        valids = []
        for i in primes_true:            
            for j in [n for n in primes_true if n > i and saved[i][n]]:                
                comb = [i, j]
                if check_concats(comb):
                    valids.append(comb)
        return valids
    else:
        prevs = check_combinations(ranks-1)      
        return check_combs_with_prev(prevs)


def initial_combs_2():
    valids = []
    for i in primes_true:
        for j in [n for n in primes_true if n > i and saved[i][n]]:               
            comb = [i, j]
            if check_concats(comb):
                valids.append(comb)
    return valids

def initial_combs_3():
    valids = []
    for i in primes_true:
        for j in [n for n in primes_true if n > i and saved[i][n]]:
            for k in [n for n in primes_true if n > j and saved[i][n] and saved[j][n]]:                
                comb = [i, j, k]
                if check_concats(comb):
                    valids.append(comb)
    return valids

def initial_combs_4():
    valids = []
    for i in primes_true:
        for j in [n for n in primes_true if n > i and saved[i][n]]:
            for k in [n for n in primes_true if n > j and saved[i][n] and saved[j][n]]:
                for m in [n for n in primes_true if n > k and saved[i][n] and saved[j][n] and saved[k][n]]:      
                    comb = [i, j, k, m]
                    if check_concats(comb):
                        valids.append(comb)
    return valids

def try_add_prime_to_comb(comb, new_prime):
    for comb_prime in comb:
        if not are_concats_prime(comb_prime, new_prime):
            return False
    return True

def check_combs_with_prev(prev_combs):
    valids = []
    for prev_comb in prev_combs:
        for prime in [i for i in primes_true if i > prev_comb[-1]]:
            saved_invalids = [index for index in prev_comb if not saved[index][prime]]
            if len(saved_invalids)==0 and prime not in prev_comb:
                sorted = (prev_comb+[prime])
                sorted.sort()                
                if sorted not in valids and try_add_prime_to_comb(prev_comb, prime):                
                    valids.append(sorted)
    return valids

# combs = check_combinations(5)
# print(combs)
print(sum([13, 5197, 5701, 6733, 8389]))
