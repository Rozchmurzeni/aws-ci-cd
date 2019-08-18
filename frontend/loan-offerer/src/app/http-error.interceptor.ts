import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse
} from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { retry, catchError } from "rxjs/operators";

export class HttpErrorInterceptor implements HttpInterceptor {
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      retry(1),
      catchError((error: HttpErrorResponse) => {
        let errorMessage = "";
        if (error.error instanceof ErrorEvent) {
          errorMessage = this.createClientSideErrorMessage(error.error);
        } else {
          errorMessage = this.createServerSideErrorMessage(error);
        }
        return throwError(errorMessage);
      })
    );
  }

  private createServerSideErrorMessage(
    httpErrorResponse: HttpErrorResponse
  ): string {
    return `Error Code: ${httpErrorResponse.status}\nMessage: ${
      httpErrorResponse.message
    }`;
  }

  private createClientSideErrorMessage(errorEvent: ErrorEvent): string {
    return `Error: ${errorEvent.message}`;
  }
}
