import math

def flatten(td_list):
    return [sequence[i] for sequence in td_list for i in range(len(sequence))]

def sieve(max):
    limit = int(max**0.5)
    primes = [1]*max
    primes[0:2]=[0]*2
    for i in range(2,limit+1):
        if primes[i]:
            last_multiple = (max-1)//i
            primes[2*i::i] = [0]*(last_multiple-1)
    return primes

# assumes possible factors of x are in sieve
def is_prime_using_sieve(x, sieve):
    last = min(len(sieve), int(x**0.5))
    for i in range(last):        
        if(sieve[i]):
            if x%i==0 and x != i:
                return False
    return True

def solve_quadratic(a,b,c):
    sqr_part = (b**2-4*a*c)**0.5
    return ((-b + sqr_part)/(2*a),(-b - sqr_part)/(2*a))

