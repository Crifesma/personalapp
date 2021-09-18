import { Role } from "../entities/Role";
import Repository from "../Repository";

export default class TestRepository extends Repository<Role> {
  constructor() {
    super("Role");
    console.log("construidad, testrepository");
  }
}
