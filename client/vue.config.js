module.exports = {
    devServer: {
      proxy: {
        '^/api': {
          target: 'http://localhost:5000',
          ws: true,
          changeOrigin: true
        },
        '^/auth': {
          target: 'http://localhost:5000',
          ws: true,
          changeOrigin: true
        },
      }
    }
  }