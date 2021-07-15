<template>
  <div>
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/welcome1' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item :to="{ path: '/dishList' }">菜单系统</el-breadcrumb-item>
      <el-breadcrumb-item>添加菜品</el-breadcrumb-item>
    </el-breadcrumb>
    <!-- 卡片视图 -->
    <el-card>
      <!-- 提示 -->
      <el-alert title="添加菜品" type="info" center show-icon :closable="false"></el-alert>
      <!-- 步骤条 -->
      <el-steps :space="200" :active="activeIndex - 0" finish-status="success" align-center>
        <el-step title="基本信息"></el-step>
        <el-step title="菜品图片url"></el-step>
        <el-step title="菜品文案"></el-step>
        <el-step title="完成"></el-step>
      </el-steps>

      <!-- Tab栏 -->
      <el-form
        :model="addForm"
        :rules="addFormRules"
        ref="addFormRef"
        label-width="100px"
        label-position="top"
      >
        <el-tabs
          v-model="activeIndex"
          :tab-position="'left'"
          :before-leave="beforeTabLeave"
          @tab-click="tabClicked"
        >
          <el-tab-pane label="基本信息" name="0">
            <el-form-item label="菜品名称" prop="dish_name">
              <el-input v-model="addForm.dish_name"></el-input>
            </el-form-item>
            <el-form-item label="菜品价格" prop="dish_price">
              <el-input v-model="addForm.dish_price" type="number"></el-input>
            </el-form-item>
            
          
          </el-tab-pane>
         
          
          <el-tab-pane label="菜品图片url" name="2">
             <el-form-item label="菜品图片url" prop="dish_url">
              <el-input v-model="addForm.dish_url" type="number"></el-input>
            </el-form-item>
          </el-tab-pane>
          <el-tab-pane label="菜品文案" name="3">
             <el-form-item label="菜品文案" prop="dish_explain">
              <el-input v-model="addForm.dish_explain"></el-input>
            </el-form-item>
             
          
            <el-button type="primary" class="btnAdd" @click="addDish">添加菜品</el-button>
          </el-tab-pane>
        </el-tabs>
      </el-form>
    </el-card>
  
  </div>
</template>

<script>
import _ from 'lodash'
export default {
  data () {
    return {
      // 步骤条默认激活 与左侧Tab联动
      activeIndex: '0',
      // 添加商品的表单对象
      addForm: {
        dish_name: '',
        dish_price: 0,
        // 图片的数组
        //pics: [],
        // 商品详情描述
        dish_url: '',
        dish_explain: '',
        
      },
      addFormRules: {
        dish_name: [
          { required: true, message: '请输入菜品名称', trigger: 'blur' }
        ],
         dish_url: [
          { required: true, message: '请输入菜品图片url', trigger: 'blur' }
        ],
         dish_explain: [
          { required: true, message: '请输入菜品文案', trigger: 'blur' }
        ],
        dish_price: [
          { required: true, message: '请输入菜品价格', trigger: 'blur' }
        ]
      },
     
    }
  },
 
  methods: {
    
    
    beforeTabLeave (activeName, odlActiveName) {
      //未选中商品分类阻止Tab标签跳转
      if (odlActiveName === '0' && this.addForm.dish_name === "") {
        this.$message.error('请先输入菜品名称')
        return false
      }
    },
 
    // 添加
    addDish () {
      this.$refs.addFormRef.validate(async valid => {
        if (!valid) return this.$message.error('请填写必要的表单项！')
       

        // 将this.addForm进行深拷贝
        const form = _.cloneDeep(this.addForm)
       

        
      const { data: res } = await this.$http.post('Dish/AddDish', form)
     
      console.log(res)
      if (res.code !== 200) {
        return this.$message.error('添加菜品失败！')
      }
      else{
        this.$message.success('添加菜品成功!')
        this.$router.push('/dishList')
      }
      })
    }
  }
}
</script>

<style scoped>
.el-checkbox {
  margin: 0 8px 0 0 !important;
}
.previewImg{
  width: 100%;
}
.btnAdd{
  margin-top: 15px
}
</style>
