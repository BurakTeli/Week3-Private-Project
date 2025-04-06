import { NextResponse } from 'next/server';
import { NextRequest } from 'next/server';

export function middleware(request: NextRequest) {
    const token = request.cookies.get('token');
    

    // Eğer kullanıcı giriş yapmamışsa, login sayfasına yönlendirilir
    if (!token && request.nextUrl.pathname !== '/login' && request.nextUrl.pathname !== '/signup') {
        return NextResponse.redirect(new URL('/login', request.url));
    }

    return NextResponse.next();
}

export const config = {
    matcher: ['/products', '/cart', '/orders', '/admin', '/order-success'],
};
