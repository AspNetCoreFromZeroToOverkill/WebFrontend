var fs = require('fs');

module.exports = {
    devServer: {
      proxy: {
        '^/api': {
          target: 'https://localhost:5001',
          ws: true,
          changeOrigin: true
        },
        '^/auth': {
          target: 'https://localhost:5001',
          ws: true,
          changeOrigin: true
        },
      },
      https: {
        key: fs.readFileSync('../../Tools/certificates/dev/localhost.key'),
        cert: fs.readFileSync('../../Tools/certificates/dev/localhost.crt')
      }
    }
  }