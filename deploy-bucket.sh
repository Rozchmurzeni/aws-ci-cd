#!/bin/bash

aws cloudformation deploy --template-file bucket.yaml --stack-name bucket-stack
aws s3 ls
