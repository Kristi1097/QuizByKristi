openssl req -new -key Certificates/private/$1_private.key -config Certificates/opensslca1.cnf -out Certificates/requests/$1.csr -verbose -batch 2>> Certificates/certslog.txt

openssl ca -in Certificates/requests/$1.csr -out Certificates/certs/$1.crt -config Certificates/opensslca1.cnf -key kristi -batch 2>> Certificates/certslog.txt

openssl ca -gencrl -out Certificates/crl/crllistca1.crl -config Certificates/opensslca1.cnf -key kristi 2>> Certificates/certslog.txt

echo "#######################################################" >> Certificates/certslog.txt