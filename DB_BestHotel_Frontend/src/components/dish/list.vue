<template >
  <div class="hello">
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/welcome1' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>菜单系统</el-breadcrumb-item>
    </el-breadcrumb>
    <div class="searchDiv">
     
       
      <el-row :gutter="5">
        <el-col :span="18">
       <el-input v-model="input" placeholder="请输入菜品名称或id"></el-input>
        </el-col>
        <el-col :span="3">
          <el-button
            class="searchBtn"
            type="primary"
            icon="el-icon-search"
            @click="search(input)"
            >搜索</el-button
          >
        </el-col>
        <el-col :span="3">
      <el-button class="addBtn" type="success" @click="goAddPage">添加菜品</el-button>
        </el-col>
      </el-row>

           
             <el-dialog title="输入修改信息" :visible.sync="dialogFormVisible">
        <el-form>
          <el-form-item label="菜品名称" :label-width="formLabelWidth">
            <el-input v-model="dishName"></el-input>
          </el-form-item>
           <el-form-item label="菜品文案" :label-width="formLabelWidth">
            <el-input v-model="dishExplain"></el-input>
          </el-form-item>
          <el-form-item label="菜品价格" :label-width="formLabelWidth">
            <el-input v-model="dishPrice"></el-input>
          </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button type="primary" @click="add(dishName,dishExplain,dishPrice)">保存</el-button>
        </div>
      </el-dialog>
    </div>
    
    
      <el-table :data="dishlist"  style="width: 100%" ref="myTable">
      
        <el-table-column type="index" :index="indexMethod"></el-table-column>
        <el-table-column prop="dish_id" label="菜品id" width="180"></el-table-column>
        <el-table-column prop="dish_name" label="菜品名称" width="180"></el-table-column>
       
        <el-table-column prop="dish_price"    sortable label="菜品价格"></el-table-column>
         <el-table-column prop="dish_explain" label="菜品文案"></el-table-column>
        <el-table-column prop="address" label="操作">
          <template slot-scope="scope">
            <el-button type="primary" @click="edit(scope.$index,scope.row)">修改</el-button>
          <el-button type="danger" @click="dele(scope.$index,scope.row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
    </div>
  
</template>

<script>
export default {
  name: "HelloWorld",
  data() {
    return {
      
      input: "",
      dishPrice: "",
      dishExplain: "",
      dishName: "",
      dishpicture: "",
      dialogFormVisible: false,
      formLabelWidth: "100px",
      form: {},
      searchData: "",
      tableData: [
        // {
        //     id:1,
        //   name: "红烧",
        //   explain: "好",
        //   price: "30.90"
        // },
        // {
        //   id:2,
        //   name: "清蒸",
        //   explain: "好",
        //   price: "29.90"
        // },
        // {
        //    id:3,
        //   name: "麻辣",
        //   explain: "好",
        //   price: "28.90"
        // },
        // {
        //    id:4,
        //   name: "酸菜",
        //   explain: "好",
        //   price: "31.90"
        // }
      ],
      oldVal: [],
      newVal: [],
      dishlist:[],
      editIndex: "",
      qubie: ""
    };
  },
  created () {
    this.getdishlist()
  },
  methods: {
    async getdishlist () {
      const { data: res } = await this.$http.post('Dish/ReturnAll_Dish')
      if (res.code !== 200) {
        return this.$message.error('获取菜单列表失败！')
      }
      this.dishlist = res.data;
        //console.log(res.data);
        // console.log(this.dishList)
     // this.total = res.data.total
    },
    formatter(row, column) {
        return row.address;
      },
      goAddPage () {
      this.$router.push('/dishAdd')
    },
        clearFilter() {
        this.$refs.myTable.clearFilter();
      },
    filterHandler(value, row, column) {
        const property = column['property'];
        return row[property] === value;
      },
    handleClose(done) {
      this.$confirm("确认关闭？")
        .then(_ => {
          done();
        })
        .catch(_ => {});
    },
    // 新增和编辑书籍功能
    async add(dishName,dishExplain, dishPrice) {
      // 编辑
      if (this.qubie == "edit") {
        // console.log(this.editIndex);
        var editdish = this.dishlist[this.editIndex];
        console.log(editdish)
       
        editdish.dish_name = dishName;
        editdish.dish_explain =dishExplain;
        editdish.dish_price = dishPrice;
        this.dialogFormVisible = false;
      const { data: res } = await this.$http.post('Dish/UpdateDish',editdish)
       console.log(editdish)
      console.log(res)
      if (res.code !== 200) {
        return this.$message.error('更新菜品失败！')
      }
        // this.dishName = "";
      }
      // 新增
      if (this.qubie == "add") {
        console.log(2);
        if (dishName == "" || dishExplain == "" || dishPrice == "") {
          this.dialogFormVisible = true;
          // console.log(1)
        } else {
          this.dialogFormVisible = false;
          // console.log(dishName, dishExplain, dishPrice);
          var newdish = {
            dish_id:1,
            dish_name: dishName,
            dish_explain: dishExplain,
            dish_price: dishPrice
          };
          this.dishlist.push(newdish);
          this.dishName = "";
          this.dishExplain = "";
          this.dishPrice = "";
        }
      }
    },
    // 删除
  async  dele(index,row) {
      // console.log(index)
 var del={ dish_id:row. dish_id};
     
      console.log(del)
 const { data: res } = await this.$http.post('Dish/DeleteDish',del)
       console.log(editdish)
      console.log(res)
      if (res.code !== 200) {
        return this.$message.error('删除菜品失败！')
      }


      this.dishlist.splice(index, 1);
    },
    // 点击编辑按钮
    edit(index, row) {
      // console.log(row.name);
      this.dialogFormVisible = true;
      this.dishName = row.dish_name;
      this.dishExplain = row.dish_explain;
      this.dishPrice = row.dish_price;
      // console.log(index)
      this.editIndex = index;
      this.qubie = "edit";
    },
    // 点击添加按钮
    toAdd() {
      this.dialogFormVisible = true;
      this.dishName = "";
      this.dishExplain = "";
      this.dishPrice = "";
      this.qubie = "add";
    },
    // 查找
    search(input) {
       console.log(input)
      var newData = [];
      this.dishlist.map((item, index) => {
         console.log(item)
        if (item.dish_name.includes(input)||item.dish_id.includes(input)) {
          console.log(item)
          newData.push(item);
          this.$refs.myTable.data = newData;
        }
      });
      return newData;
      // console.log(this.tableData)
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="less">

</style>
