#!/bin/bash
# auto dotnet test runner by rpopic2
# last edit 23 Aug 2022
DIR=$(dirname "$0")
echo $DIR
date
echo Starting tests
rm testlist 2> /dev/null
list=$(find .. -name \*Test.cs)
for testitem in $list; do
    tempname=$(basename $testitem .cs)
    echo $tempname >> testlist
    echo Found test file $tempname
done

EXITCODE=0
for testitem2 in `cat testlist`; do
    echo -------------- $testitem2 -------------- 
    dotnet test $DIR --nologo --verbosity=q --no-build --filter=$testitem2
    if [ $? != 0 ]; then
        EXITCODE=1
        FAILED_TESTS="${FAILED_TESTS}\n$testitem2 failed!"
    fi
done
rm testlist 2> /dev/null
if [[ -n $FAILED_TESTS ]]; then
   >&2 echo -e "\e[31m${FAILED_TESTS}\e[0m"
else
    echo All tests succeeded.
fi
exit $EXITCODE