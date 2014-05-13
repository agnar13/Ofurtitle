#include <iostream>
#include <cstdarg>

using namespace std;

const int MAX_LENGTH = 100;

int max(int a, int b, int c)
{
    return max(a,max(b,c));
}

int max(int a, int b, int c, int d)
{
    return max(a,max(b,c,d));
}

int opt(int A[], int n)
{
    switch(n)
    {
        case 0:         // Empty list
            return 0;
        case 1:         // Single element list
            return A[0];
        case 2:
            return max(A[0], A[1]);
        case 3:         // Problem 3
            return -1;
        case 4:         // Problem 4
            return -1;
        case 5:         // Problem 5
            return -1;
        case 6:         // Problem 6
            return -1;
        default:
            return -1;
    }
}


int main()
{
    int test_cases, n;
    int A[MAX_LENGTH];

    cin >> test_cases;
    for(int t = 0; t != test_cases; t++)
    {
        cin >> n;
        for(int i = 0; i < n; i++)
        {
            cin >> A[i];
        }

        cout << opt(A, n) << endl;
    }
    return 0;
}
