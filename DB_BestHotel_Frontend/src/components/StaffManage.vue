<template>
  <div>
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/welcome1' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>员工系统</el-breadcrumb-item>
    </el-breadcrumb>
    <div class="searchDiv">
      <el-row :gutter="5">
        <el-col :span="18">
          <el-input
            v-model="input"
            placeholder="请输入员工相关信息：员工编号，姓名，部门，职位"
          ></el-input>
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
          <el-button type="success" @click="toAdd">添加员工</el-button>
        </el-col>
      </el-row>

      <el-dialog title="员工信息" :visible.sync="dialogFormVisible" width="80%">
        <el-form
          label-position="left"
          label-width="150px"
          :rules="staffFormRules"
          :model="staffForm"
          ref="loginFormRef111"
        >
          <el-form-item label="员工姓名：" prop="staff_name">
            <el-input v-model="staffForm.staff_name"></el-input>
          </el-form-item>

          <el-form-item label="性别：" prop="staff_sex">
            <!-- <el-input v-model="staffForm.staff_sex"></el-input> -->
            <el-select
              v-model="staffForm.staff_sex"
              placeholder="请选择员工性别"
            >
              <el-option label="男" value="男"></el-option>
              <el-option label="女" value="女"></el-option>
            </el-select>
          </el-form-item>

          <el-form-item label="年龄：" prop="staff_age">
            <el-input v-model.number="staffForm.staff_age"></el-input>
          </el-form-item>

          <el-form-item
            label="员工身份证号码："
            prop="staff_identity_card_number"
          >
            <el-input v-model="staffForm.staff_identity_card_number"></el-input>
          </el-form-item>

          <el-form-item label="员工住址：" prop="staff_address">
            <el-input v-model="staffForm.staff_address"></el-input>
          </el-form-item>

          <el-form-item label="员工所属部门：" prop="staff_department">
            <el-input v-model="staffForm.staff_department"></el-input>
          </el-form-item>

          <el-form-item label="上司员工编号：" prop="leader_id">
            <el-input v-model="staffForm.leader_id"></el-input>
          </el-form-item>

          <el-form-item label="员工职位：" prop="staff_position">
            <el-input v-model="staffForm.staff_position"></el-input>
          </el-form-item>

          <el-form-item label="员工入职时间：" prop="staff_entry_date">
            <el-date-picker
              type="date"
              placeholder="选择入职日期"
              value-format="yyyy-MM-dd"
              v-model="staffForm.staff_entry_date"
              style="width: 100%"
            ></el-date-picker>
          </el-form-item>

          <el-form-item label="员工薪水：" prop="staff_salary">
            <el-input v-model.number="staffForm.staff_salary"></el-input>
          </el-form-item>

          <el-form-item>
            <el-button
              type="primary"
              @click="
                add(
                  staffForm.staff_name,
                  staffForm.staff_sex,
                  staffForm.staff_age,
                  staffForm.staff_identity_card_number,
                  staffForm.staff_address,
                  staffForm.staff_department,
                  staffForm.leader_id,
                  staffForm.staff_position,
                  staffForm.staff_entry_date,
                  staffForm.staff_salary
                )
              "
              >保存</el-button
            >
          </el-form-item>
        </el-form>
      </el-dialog>
    </div>
    <div class="tableDiv">
      <el-table
        :data="tableData"
        style="width: 100%"
        ref="myTable"
        stripe
        fixed
      >
        <el-table-column
          prop="staff_id"
          label="员工编号"
          width="100"
          sortable
        ></el-table-column>
        <el-table-column
          prop="staff_name"
          label="员工姓名"
          width="100"
          fixed="left"
          sortable
        ></el-table-column>
        <el-table-column
          prop="staff_sex"
          label="性别"
          width="60"
          :filters="[
            { text: '男', value: '男' },
            { text: '女', value: '女' },
          ]"
          :filter-method="filterHandler"
        ></el-table-column>
        <el-table-column
          prop="staff_age"
          label="年龄"
          width="80"
          sortable
        ></el-table-column>
        <el-table-column
          prop="staff_identity_card_number"
          label="员工身份证号码"
          min-width="170"
        ></el-table-column>
        <el-table-column
          prop="staff_address"
          label="员工住址"
          width="120"
        ></el-table-column>
        <el-table-column
          prop="staff_department"
          label="员工所属部门"
          width="120"
        ></el-table-column>
        <el-table-column
          prop="leader_id"
          label="上司员工编号"
          width="110"
        ></el-table-column>
        <el-table-column
          prop="staff_position"
          label="员工职位"
          width="120"
        ></el-table-column>
        <el-table-column
          prop="staff_entry_date"
          label="员工入职时间"
          min-width="130"
          sortable
        ></el-table-column>
        <el-table-column
          prop="staff_salary"
          label="员工薪水"
          width="120"
          sortable
        ></el-table-column>

        <el-table-column prop="address" label="操作" width="175" fixed="right">
          <template slot-scope="scope">
            <el-button type="primary" @click="edit(scope.$index, scope.row)"
              >修改</el-button
            >
            <el-button type="danger" @click="confirmDelete(scope.$index)"
              >删除</el-button
            >
          </template>
        </el-table-column>
      </el-table>
    </div>
  </div>
</template>

<style>
el-breadcrumb-item {
  margin: 0 auto;
}
.searchDiv {
  padding: 10px;
}
.tableDiv {
  padding: 10px;
}
</style>

<script>
export default {
  name: "HelloWorld",
  data() {
    return {
      input: "",

      editIndex: "",

      qubie: "",

      dialogFormVisible: false,

      staffForm: {
        staff_name: "adm",
        staff_sex: "",
        staff_age: "",
        staff_identity_card_number: "",
        staff_address: "",
        staff_department: "",
        leader_id: "",
        staff_position: "",
        staff_entry_date: "",
        staff_salary: "",
      },

      staffFormRules: {
        //用户名

        staff_name: [
          { required: true, message: "请输入员工名", trigger: "blur" },
          {
            max: 10,
            message: "长度不能大于10个字符",
            trigger: "blur",
          },
        ],

        staff_sex: [
          { required: true, message: "请输入员工性别", trigger: "blur" },
          {
            type: "enum",
            enum: ["男", "女"],
            message: "性别只能是男和女",
            trigger: "blur",
          },
        ],
        staff_age: [
          { required: true, message: "请输入员工年龄", trigger: "blur" },
          {
            type: "number",
            min: 16,
            max: 65,
            message: "员工年龄需要在16～65周岁之间",
            trigger: "blur",
          },
        ],
        staff_identity_card_number: [
          { required: true, message: "请输入员工身份证号码", trigger: "blur" },
          {
            len: 18,
            message: "请输入有效的身份证号",
            trigger: "blur",
          },
        ],
        staff_address: [
          { required: true, message: "请输入员工住址", trigger: "blur" },
          {
            max: 30,
            message: "长度不能大于30",
            trigger: "blur",
          },
        ],
        staff_department: [
          { required: true, message: "请输入员工部门", trigger: "blur" },
          {
            max: 10,
            message: "长度不能大于10",
            trigger: "blur",
          },
        ],
        // leader_id: [
        //   { required: true, message: "请输入上司员工编号", trigger: "blur" },
        //   {
        //     len: 5,
        //     message: "请输入有效的员工编号",
        //     trigger: "blur",
        //   },
        // ],

        staff_position: [
          { required: true, message: "请输入员工职位", trigger: "blur" },
          {
            max: 10,
            message: "长度不能大于10",
            trigger: "blur",
          },
        ],
        staff_entry_date: [
          { required: true, message: "请输入员工入职日期", trigger: "blur" },
        ],
        staff_salary: [
          { required: true, message: "请输入员工薪水", trigger: "blur" },
          {
            type: "number",
            min: 0,
            message: "需要一个正整数",
            trigger: "blur",
          },
        ],
      },

      tableData: [
        // {
        //   staff_id: "11111",
        //   staff_name: "张三",
        //   staff_sex: "男",
        //   staff_age: 20,
        //   staff_identity_card_number: "429000000000000000",
        //   staff_address: "20号楼",
        //   staff_department: "保洁部",
        //   leader_id: "12345",
        //   staff_position: "洗碗工",
        //   staff_entry_date: "2020-01-20",
        //   staff_salary: 10000000,
        // },
        // {
        //   staff_id: "12345",
        //   staff_name: "莉丝",
        //   staff_sex: "女",
        //   staff_age: 29,
        //   staff_identity_card_number: "429000000011100000",
        //   staff_address: "19号楼",
        //   staff_department: "保安部",
        //   leader_id: "12345",
        //   staff_position: "保安队长",
        //   staff_entry_date: "2020-01-29",
        //   staff_salary: 900000,
        // },
        // {
        //   staff_id: "10000",
        //   staff_name: "TONY",
        //   staff_sex: "女",
        //   staff_age: 29,
        //   staff_identity_card_number: "429000000000002222",
        //   staff_address: "20号楼",
        //   staff_department: "保洁部",
        //   leader_id: "12345",
        //   staff_position: "洗碟工",
        //   staff_entry_date: "2020-03-20",
        //   staff_salary: 8987898,
        // },
        // {
        //   staff_id: "17777",
        //   staff_name: "张四",
        //   staff_sex: "男",
        //   staff_age: 30,
        //   staff_identity_card_number: "429000000006666000",
        //   staff_address: "20号楼",
        //   staff_department: "餐饮部",
        //   leader_id: "12345",
        //   staff_position: "厨师长",
        //   staff_entry_date: "2020-01-20",
        //   staff_salary: 99800000,
        // },
        // {
        //   staff_id: "21112",
        //   staff_name: "张三",
        //   staff_sex: "男",
        //   staff_age: 20,
        //   staff_identity_card_number: "429000000000000000",
        //   staff_address: "20号楼",
        //   staff_department: "保洁部",
        //   leader_id: "12345",
        //   staff_position: "洗碗工",
        //   staff_entry_date: "2020-01-20",
        //   staff_salary: 10000000,
        // },
        // {
        //   staff_id: "21113",
        //   staff_name: "张三",
        //   staff_sex: "男",
        //   staff_age: 20,
        //   staff_identity_card_number: "429000000000000000",
        //   staff_address: "20号楼",
        //   staff_department: "保洁部",
        //   leader_id: "12345",
        //   staff_position: "洗碗工",
        //   staff_entry_date: "2020-01-20",
        //   staff_salary: 10000000,
        // },
        // {
        //   staff_id: "21114",
        //   staff_name: "张三",
        //   staff_sex: "男",
        //   staff_age: 20,
        //   staff_identity_card_number: "429000000000000000",
        //   staff_address: "20号楼",
        //   staff_department: "保洁部",
        //   leader_id: "12345",
        //   staff_position: "洗碗工",
        //   staff_entry_date: "2020-01-20",
        //   staff_salary: 10000000,
        // },
        // {
        //   staff_id: "21115",
        //   staff_name: "张三",
        //   staff_sex: "男",
        //   staff_age: 20,
        //   staff_identity_card_number: "429000000000000000",
        //   staff_address: "20号楼",
        //   staff_department: "保洁部",
        //   leader_id: "12345",
        //   staff_position: "洗碗工",
        //   staff_entry_date: "2020-01-20",
        //   staff_salary: 10000000,
        // },
        // {
        //   staff_id: "21116",
        //   staff_name: "张三",
        //   staff_sex: "男",
        //   staff_age: 20,
        //   staff_identity_card_number: "429000000000000000",
        //   staff_address: "20号楼",
        //   staff_department: "保洁部",
        //   leader_id: "12345",
        //   staff_position: "洗碗工",
        //   staff_entry_date: "2020-01-20",
        //   staff_salary: 10000000,
        // },
        // {
        //   staff_id: "21117",
        //   staff_name: "张三",
        //   staff_sex: "男",
        //   staff_age: 20,
        //   staff_identity_card_number: "429000000000000000",
        //   staff_address: "20号楼",
        //   staff_department: "保洁部",
        //   leader_id: "12345",
        //   staff_position: "洗碗工",
        //   staff_entry_date: "2020-01-20",
        //   staff_salary: 10000000,
        // },
        // {
        //   staff_id: "21118",
        //   staff_name: "张三",
        //   staff_sex: "男",
        //   staff_age: 20,
        //   staff_identity_card_number: "429000000000000000",
        //   staff_address: "20号楼",
        //   staff_department: "保洁部",
        //   leader_id: "12345",
        //   staff_position: "洗碗工",
        //   staff_entry_date: "2020-01-20",
        //   staff_salary: 10000000,
        // },
        // {
        //   staff_id: "21119",
        //   staff_name: "张三",
        //   staff_sex: "男",
        //   staff_age: 20,
        //   staff_identity_card_number: "429000000000000000",
        //   staff_address: "20号楼",
        //   staff_department: "保洁部",
        //   leader_id: "12345",
        //   staff_position: "洗碗工",
        //   staff_entry_date: "2020-01-20",
        //   staff_salary: 10000000,
        // },
      ],
    };
  },
  created: function () {
    this.getData();
  },
  methods: {
    handleClose(done) {
      this.$confirm("确认关闭？")
        .then((_) => {
          done();
        })
        .catch((_) => {});
    },
    handleData1() {
      for (let i = 0; i < this.tableData.length; i++) {
        this.tableData[i].staff_age = parseInt(this.tableData[i].staff_age);
        this.tableData[i].staff_salary = parseInt(
          this.tableData[i].staff_salary
        );

        this.tableData[i].staff_entry_date = this.tableData[
          i
        ].staff_entry_date.substring(
          0,
          this.tableData[i].staff_entry_date.length - 8
        );
        this.tableData[i].staff_entry_date = this.tableData[
          i
        ].staff_entry_date.replace("/", "-");
        this.tableData[i].staff_entry_date = this.tableData[
          i
        ].staff_entry_date.replace("/", "-");
      }
    },

    async getData() {
      // const res = await this.$http.post("try/ajax/demo_axios_post.php");
      const res = await this.$http.post("Staff/ReturnAll");

      if (res.status !== 200) this.$message.error("获取数据失败");
      if (res.data.code !== 200) this.$message.error("加载失败");

      // console.log(res);
      // console.log(res.staff);
      // console.log(res.data);
      this.tableData = res.data.data;
      this.handleData1();

      //TODO
      // this.tableData = res.staffs;
    },
    // 新增和编辑书籍功能
    add(
      staff_name,
      staff_sex,
      staff_age,
      staff_identity_card_number,
      staff_address,
      staff_department,
      leader_id,
      staff_position,
      staff_entry_date,
      staff_salary
    ) {
      this.$refs.loginFormRef111.validate(async (validTag) => {
        //判断bool形参来预验证登陆
        // console.log(validTag);
        if (!validTag) {
          return;
        }

        if (this.qubie == "edit") {
          // console.log(this.editIndex);
          var editBook = this.tableData[this.editIndex];
          // console.log(editBook);

          staff_entry_date.replace("-", "/");
          staff_entry_date.replace("-", "/");
          var postStaff = {
            staff_id: editBook.staff_id,
            staff_name: staff_name,
            staff_sex: staff_sex,
            staff_age: staff_age.toString(),
            staff_identity_card_number: staff_identity_card_number,
            staff_address: staff_address,
            staff_department: staff_department,
            leader_id: leader_id,
            staff_position: staff_position,
            staff_entry_date: staff_entry_date,
            staff_salary: staff_salary.toString(),
          };
          console.log(postStaff);
          const res = await this.$http.post("Staff/StaffUpdate", postStaff);
          console.log(res);
          //TODO

          if (res.status !== 200) return this.$message.error("获取数据失败");
          if (res.data.code !== 200) {
            this.$message.error("修改失败");
          } else {
            this.$message.success("修改成功");
          }

          this.getData();
          // editBook.staff_name = staff_name;
          // editBook.staff_sex = staff_sex;
          // editBook.staff_age = staff_age;
          // editBook.staff_identity_card_number = staff_identity_card_number;
          // editBook.staff_address = staff_address;
          // editBook.staff_department = staff_department;
          // editBook.leader_id = leader_id;
          // editBook.staff_position = staff_position;
          // editBook.staff_entry_date = staff_entry_date;
          // editBook.staff_salary = staff_salary;

          this.dialogFormVisible = false;
        }
        // 新增
        if (this.qubie == "add") {
          var maxID = 0;
          for (let i = 0; i < this.tableData.length; i++) {
            if (parseInt(this.tableData[i].staff_id) > maxID) {
              maxID = parseInt(this.tableData[i].staff_id);
            }
          }
          maxID += 1;
          console.log(maxID);

          staff_entry_date.replace("-", "/");
          staff_entry_date.replace("-", "/");
          var postStaff = {
            staff_id: maxID.toString(),
            staff_name: staff_name,
            staff_sex: staff_sex,
            staff_age: staff_age.toString(),
            staff_identity_card_number: staff_identity_card_number,
            staff_address: staff_address,
            staff_department: staff_department,
            leader_id: leader_id,
            staff_position: staff_position,
            staff_entry_date: staff_entry_date,
            staff_salary: staff_salary.toString(),
          };
          // console.log(postStaff);

          const res = await this.$http.post("Staff/StaffAdd", postStaff);
          console.log(res);

          if (res.status !== 200) return this.$message.error("获取数据失败");
          if (res.data.code !== 200) {
            this.$message.error("添加失败");
          } else {
            this.$message.success("添加成功");
          }

          //TODO

          var tempStaff = {
            staff_id: maxID.toString(), //未允许编辑的值
            staff_name: staff_name,
            staff_sex: staff_sex,
            staff_age: staff_age,
            staff_identity_card_number: staff_identity_card_number,
            staff_address: staff_address,
            staff_department: staff_department,
            leader_id: leader_id,
            staff_position: staff_position,
            staff_entry_date: staff_entry_date,
            staff_salary: staff_salary,
          };

          this.getData();
          // this.tableData.push(tempStaff);
          // this.tableData.

          this.dialogFormVisible = false;
        }
      });
    },

    filterHandler(value, row, column) {
      const property = column["property"];
      return row[property] === value;
    },
    confirmDelete(index) {
      this.$confirm("此操作将永久删除该员工, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          this.dele(index);
          // this.$message({
          //   type: "success",
          //   message: "删除成功!",
          // });
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消删除",
          });
        });
    },

    // 删除
    async dele(index) {
      console.log(index);
      console.log({ staff_id: this.tableData[index].staff_id });
      const res = await this.$http.post("Staff/StaffDelete", {
        staff_id: this.tableData[index].staff_id,
      });
      console.log(res);
      if (res.status !== 200) return this.$message.error("获取数据失败");
      if (res.data.code !== 200) {
        this.$message.error("删除失败");
      } else {
        this.$message.success("删除成功");
      }

      //TODO

      this.tableData.splice(index, 1);
    },
    // 点击编辑按钮
    edit(index, row) {
      // console.log(index);

      this.$nextTick(() => {
        this.$refs["loginFormRef111"].clearValidate(); //重置表单状态
      });

      this.dialogFormVisible = true;
      var tempStaff = {
        staff_name: row.staff_name,
        staff_sex: row.staff_sex,
        staff_age: row.staff_age,
        staff_identity_card_number: row.staff_identity_card_number,
        staff_address: row.staff_address,
        staff_department: row.staff_department,
        leader_id: row.leader_id,
        staff_position: row.staff_position,
        staff_entry_date: row.staff_entry_date,
        staff_salary: row.staff_salary,
      };
      this.staffForm = tempStaff;

      // console.log(index)
      this.editIndex = index;
      this.qubie = "edit";
    },
    // 点击添加按钮
    async toAdd() {
      this.dialogFormVisible = true;

      this.$nextTick(() => {
        this.$refs["loginFormRef111"].clearValidate(); //重置表单状态
      });

      var tempStaff = {
        staff_name: "",
        staff_sex: "",
        staff_age: "",
        staff_identity_card_number: "",
        staff_address: "",
        staff_department: "",
        leader_id: "",
        staff_position: "",
        staff_entry_date: "",
        staff_salary: "",
      };

      this.staffForm = tempStaff;

      // const { data: data } = await this.$http.get("try/ajax/json_demo.json");
      const result = await this.$http.get("try/ajax/json_demo.json");

      console.log(result);
      console.log(result.data);
      console.log(result.data.sites);
      console.log(result.data.sites[0]);
      console.log(result.data.sites[0].info[0]);

      this.qubie = "add";

      window.sessionStorage.setItem("staff", "fdsjhf");
      const tokenStr = window.sessionStorage.getItem("staff");
      console.log(tokenStr);
      // console.log(result.status);
      // console.log(result.sites[0]);
    },
    // 查找
    search(input) {
      // console.log(input)
      var newData = [];
      this.tableData.map((item, index) => {
        if (
          item.staff_id.includes(input) ||
          item.staff_name.includes(input) ||
          item.staff_department.includes(input) ||
          item.staff_position.includes(input)
        ) {
          newData.push(item);
          this.$refs.myTable.data = newData;
        }
      });
      return newData;
      // console.log(this.tableData)
    },
  },
};
</script>
