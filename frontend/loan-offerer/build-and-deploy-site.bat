npm install
npm install -g @angular/cli
ng build --outputHashing=all
aws s3 sync ./dist/loan-offerer/ s3://<PACKAGE_BUCKET> --delete


