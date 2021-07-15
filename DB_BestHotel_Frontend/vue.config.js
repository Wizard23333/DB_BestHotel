module.exports = {
   
    devServer: {
        open: true,
        host: 'localhost',
        port: 8080,
        https: false,
        //以上的ip和端口是我们本机的;下面为需要跨域的
        proxy: { //配置跨域
            '/shiro': {  // 这里和方法里 axios里的 url匹配   （本行为第9行）
                target: 'http://192.168.43.41:81/api', //这里后台的地址模拟的;应该填写你们真实的后台接口  // 这里和后台地址完全匹配
                ws: true,
                changOrigin: true, //允许跨域
                pathRewrite: {
                    '^/shiro': '' //请求的时候使用这个api就可以 // 这里和  第9行匹配
                }
            }
        },
        // proxyTable:{
        //     '/api':{
        //         target:'http://192.168.43.41:81/api',
        //         changOrigin:true,
        //         pathRewrite:{
        //             '^/api':'/api'
        //         }
        //     }
        // },
    },
    configureWebpack: {

        performance: {
            hints: 'warning',
            //入口起点的最大体积
            maxEntrypointSize: 50000000,
            //生成文件的最大体积
            maxAssetSize: 30000000,
            //只给出 js 文件的性能提示
            assetFilter: function(assetFilename) {
                return assetFilename.endsWith('.js');
            }
        }
    },
    publicPath: './',

}
