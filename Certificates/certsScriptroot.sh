openssl genrsa -out Certificates/private/private4096.key 2>> Certificates/keyslog.txt

openssl req -x509 -new -key Certificates/private/private4096.key -config Certificates/openssl.cnf -out Certificates/requests/$1.csr -verbose -batch 2>> Certificates/certslog.txt

echo "#######################################################" >> Certificates/certslog.txt