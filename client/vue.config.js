module.exports = {
    devServer: {
      proxy: {
        '**': {
          filter: (pathname, req) => pathname.match('^/api') || pathname.match('^/auth'),
          target: 'http://localhost:5000',
          ws: true,
          changeOrigin: true
        }
      }
    }
  }